﻿using System;
using System.Collections.Generic;
using SkiaSharp;

namespace KimonoCore
{
	/// <summary>
	/// A vector is a hard edged shape consisting of a set of datapoints that will be drawn into the Desing Surface.
	/// </summary>
	public class KimonoShapeVector : KimonoShape
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the points that define the shape.
		/// </summary>
		/// <value>The points.</value>
		public List<SKPoint> Points { get; set; } = new List<SKPoint>();

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:KimonoCore.KimonoShapeVector"/> is closed.
		/// </summary>
		/// <value><c>true</c> if closed; otherwise, <c>false</c>.</value>
		public bool Closed { get; set; } = true;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:KimonoCore.KimonoShapeVector"/> is editable.
		/// </summary>
		/// <value><c>true</c> if is editable; otherwise, <c>false</c>.</value>
		public override bool IsEditable
		{
			get { return true; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoShapeVector"/> class.
		/// </summary>
		public KimonoShapeVector()
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoShapeVector"/> class.
		/// </summary>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="right">Right.</param>
		/// <param name="bottom">Bottom.</param>
		public KimonoShapeVector(float left, float top, float right, float bottom) : base(left, top, right, bottom)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoShapeVector"/> class.
		/// </summary>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="right">Right.</param>
		/// <param name="bottom">Bottom.</param>
		/// <param name="state">State.</param>
		public KimonoShapeVector(float left, float top, float right, float bottom, KimonoShapeState state) : base(left, top, right, bottom, state)
		{
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize()
		{
			// Set the default properties
			Name = "Vector";
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Recalculates the vector bounds to encompas all of the data points that define the shape.
		/// </summary>
		public void RecalculateVectorBounds()
		{
			// Anything to process?
			if (Points.Count < 1) return;

			// Reset the bounding rect
			Left = Points[0].X;
			Right = Points[0].X;
			Top = Points[0].Y;
			Bottom = Points[0].Y;

			// Process all points to find the bounds
			foreach (SKPoint point in Points)
			{
				// Adjust bounds for this point
				if (point.X < Left) Left = point.X;
				if (point.X > Right) Right = point.X;
				if (point.Y < Top) Top = point.Y;
				if (point.Y > Bottom) Bottom = point.Y;
			}
		}

		/// <summary>
		/// Adds a point to the shape.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void AddPoint(float x, float y)
		{
			// Add point and recalculate bounds
			Points.Add(new SKPoint(x, y));
		}

		/// <summary>
		/// Adds the given point to the shape.
		/// </summary>
		/// <param name="point">The new <c>SKPoint</c> to add.</param>
		public void AddPoint(SKPoint point)
		{
			// is this the first point added?
			if (Points.Count > 0)
			{
				// No, grab first point
				var firstPoint = Points[0];

				// Is the new point within five pixels of the first point?
				if (point.X > (firstPoint.X - 5) && point.X < (firstPoint.X + 5) &&
					point.Y > (firstPoint.Y - 5) && point.Y < (firstPoint.Y + 5))
				{
					// Yes, close the shape and complete editing
					point = firstPoint;
					State = KimonoShapeState.Finalizing;
				}
			}

			// Add point and recalculate bounds
			Points.Add(point);
			RecalculateVectorBounds();

			// Are we in the editing mode?
			if (State == KimonoShapeState.Editing || State == KimonoShapeState.Finalizing)
			{
				// Add a new drag handle for the new point
				ControlPoints.Add(new KimonoHandle(point.X - KimonoHandle.DrawOffset, point.Y - KimonoHandle.DrawOffset));
			}
		}

		/// <summary>
		/// Moves the last point added to the new position.
		/// </summary>
		/// <param name="point">The <c>SKPoint</c> for the new position.</param>
		public void MoveLastPoint(SKPoint point)
		{
			// Anything to process?
			if (Points.Count < 1) return;

			// Move last point to match new location
			Points[Points.Count - 1] = point;
			RecalculateVectorBounds();
		}

		/// <summary>
		/// Removes the given point.
		/// </summary>
		/// <param name="n">The index of the point ot remove.</param>
		public void RemovePoint(int n)
		{
			// Remove point and recalculate bounds
			Points.RemoveAt(n);
			RecalculateVectorBounds();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Applies the move and scale amount to the data point that make up the shape.
		/// </summary>
		private void ApplyMoveAndScale()
		{
			// Calculate origin offest
			var originOffest = new SKSize(PreviousRect.Left - Rect.Left, PreviousRect.Top - Rect.Top);

			// Get new center
			var center = Center;

			// Calculate offsets
			var offest = new SKSize(Rect.Left - PreviousRect.Left, Rect.Top - PreviousRect.Top);

			// Apply offest to all points
			for (int n = 0; n < Points.Count; ++n)
			{
				Points[n] = SKPoint.Add(Points[n], offest);
			}

			// Calculate Size change
			var offsetX = 0f;
			var shrinkX = true;
			var offsetY = 0f;
			var shrinkY = true;

			if (Rect.Width < PreviousRect.Width)
			{
				offsetX = Rect.Width / PreviousRect.Width;
				shrinkX = true;
			}
			else
			{
				offsetX = PreviousRect.Width / Rect.Width;
				shrinkX = false;
			}

			if (Rect.Height < PreviousRect.Height)
			{
				offsetY = Rect.Height / PreviousRect.Height;
				shrinkY = true;
			}
			else
			{
				offsetY = PreviousRect.Height / Rect.Height;
				shrinkY = false;
			}

			if (!((int)offsetX == 1 && (int)offsetY == 1))
			{
				// Apply offest to all points
				for (int n = 0; n < Points.Count; ++n)
				{
					// Get adjusted x,y
					var x = Points[n].X - PreviousCenter.X + originOffest.Width;
					var y = Points[n].Y - PreviousCenter.Y + originOffest.Height;

					// Calculate resized positions
					if (shrinkX)
					{
						x = (x * offsetX) + center.X;
					}
					else
					{
						x = (x / offsetX) + center.X;
					}
					if (shrinkY)
					{
						y = (y * offsetY) + center.Y;
					}
					else
					{
						y = (y / offsetY) + center.Y;
					}

					// Apply new point
					Points[n] = new SKPoint(x, y);
				}
			}
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draws the shape into the given Skia Canvas.
		/// </summary>
		/// <param name="canvas">The <c>SKCanvas</c> to draw into.</param>
		public override void Draw(SKCanvas canvas)
		{
			// Rotated?
			if (RotationDegrees > 0)
			{
				// Save current state and apply rotation
				canvas.Save();
				canvas.RotateDegrees(RotationDegrees, HorizontalCenter, VerticalCenter);
			}

			// Anything to draw?
			if (Points.Count > 1)
			{
				// Yes, define path
				var path = new SKPath();
				path.MoveTo(Points[0]);
				for (int n = 1; n < Points.Count; n++)
				{
					path.LineTo(Points[n]);
				}
				if (Closed) path.LineTo(Points[0]);

				// Draw shape
				if (Visible)
				{
					if (Style.HasFill)
					{
						ConformGradientToShape(Style.FillGradient, Style.Fill);
						canvas.DrawPath(path, Style.Fill);
					}
					if (Style.HasFrame)
					{
						ConformGradientToShape(Style.FrameGradient, Style.Frame);
						canvas.DrawPath(path, Style.Frame);
					}
				}
			}

			// Are we in the editing mode?
			if (State == KimonoShapeState.Editing)
			{
				// Draw control point handles
				foreach (KimonoHandle handle in ControlPoints)
				{
					handle.Draw(canvas);
				}
			}

			// Call base to draw bounds if required
			base.Draw(canvas);

			// Rotated?
			if (RotationDegrees > 0)
			{
				// Restore previous state
				canvas.Restore();
			}
		}

		/// <summary>
		/// Tests to see if the given point is inside the bounds of the shape.
		/// </summary>
		/// <returns><c>true</c>, if in bound was pointed, <c>false</c> otherwise.</returns>
		/// <param name="point">The <c>SKPoint</c> to test.</param>
		public override bool PointInBound(SKPoint point)
		{
			// In the editing mode?
			if (State == KimonoShapeState.Editing)
			{
				// Yes, see if any handle has been hit
				HitHandle = null;
				foreach (KimonoHandle handle in ControlPoints)
				{
					if (handle.PointInBound(point))
					{
						HitHandle = handle;
						return true;
					}
				}
			}

			// Default to parent
			return base.PointInBound(point);
		}

		/// <summary>
		/// Drags the bounds of the shape to a new location.
		/// </summary>
		/// <param name="point">The <c>SKPoint</c> to drag the bounds to.</param>
		public override void DragBounds(SKPoint point)
		{
			// In the editing mode?
			if (State == KimonoShapeState.Editing || State == KimonoShapeState.Finalizing)
			{
				// Is a handle available?
				if (HitHandle != null)
				{
					HitHandle.DragHandle(point);
					RecalculateVectorBounds();
				}
				return;
			}

			// Allow parent to move and size bounds
			base.DragBounds(point);
		}

		/// <summary>
		/// Handles the bounds of the shape being changed.
		/// </summary>
		public override void BoundsChanged()
		{
			// Editing shape?
			if (State == KimonoShapeState.Editing || State == KimonoShapeState.Finalizing)
			{
				// Yes, allow parent to adjust bounds
				base.BoundsChanged();
			}
			else
			{
				// No, ensure that the bounds stay positive
				if (Rect.Left > Rect.Right)
				{
					Rect.Right = Rect.Left + 1;
				}

				if (Rect.Top > Rect.Bottom)
				{
					Rect.Bottom = Rect.Top + 1;
				}

				// Allow parent to adjust bounds
				base.BoundsChanged();

				// Apply changes
				ApplyMoveAndScale();
			}
		}

		/// <summary>
		/// Places the shape into the editing mode where the user can adjust the individual data points
		/// that define the shape.
		/// </summary>
		public override void StartEditing()
		{
			base.StartEditing();

			// Add the required control points
			for (int n = 0; n < Points.Count; ++n)
			{
				var point = Points[n];
				var handle = new KimonoHandle(point.X - KimonoHandle.DrawOffset, point.Y - KimonoHandle.DrawOffset);
				handle.Index = n;
				handle.Moved += (pt) =>
				{
					// Move attached point
					Points[handle.Index] = pt;
				};
				ControlPoints.Add(handle);
			}
		}
		#endregion

		#region Cloning
		/// <summary>
		/// Clone this instance.
		/// </summary>
		/// <returns>The clone <c>KimonoShapeVector</c>.</returns>
		public override KimonoBounds Clone()
		{
			// Duplicate shape
			var newShape = new KimonoShapeVector(this.Left, this.Top, this.Right, this.Bottom, this.State)
			{
				UniqueID = this.UniqueID,
				Name = this.Name,
				Style = CloneAttachedStyle(),
				Visible = this.Visible,
				Closed = this.Closed,
				LayerDepth = this.LayerDepth
			};

			// Clone control points
			foreach (KimonoHandle handle in ControlPoints)
			{
				// Duplicate handle and add to collection
				newShape.ControlPoints.Add(handle.Clone());
			}

			// Copy points
			foreach (SKPoint point in Points)
			{
				newShape.Points.Add(point);
			}

			// Return new shape
			return newShape;
		}
		#endregion
	}
}
