﻿using System;
using SkiaSharp;

namespace KimonoCore
{
	/// <summary>
	/// Holds a named <c>SKColor</c> that can be a static value or computed off of another
	/// <c>KimonoColor</c>. A <c>KimonoColor</c> can be attached directly to a <c>KimonoShape</c>
	/// or be used in a <c>KimonoStyle<c>.
	/// </summary>
	public class KimonoColor
	{
		#region Computed Static Properties
		// Primary colors
		public static SKColor White { get { return new SKColor(255, 255, 255); } }
		public static SKColor LightGray { get { return new SKColor(230, 230, 230); } }
		public static SKColor Gray { get { return new SKColor(128, 128, 128); } }
		public static SKColor DarkGray { get { return new SKColor(25, 25, 25); } }
		public static SKColor Black { get { return new SKColor(0, 0, 0); } }
		public static SKColor Red { get { return new SKColor(255, 0, 0); } }
		public static SKColor Green { get { return new SKColor(0, 255, 0); } }
		public static SKColor Blue { get { return new SKColor(0, 0, 255); } }
		public static SKColor Purple { get { return new SKColor(128, 0, 255); } }
		public static SKColor Orange { get { return new SKColor(255, 128, 0); } }
		public static SKColor Yellow { get { return new SKColor(255, 255, 102); } }
		public static SKColor Pink { get { return new SKColor(255, 111, 207); } }

		// Apple's "Crayon Box" colors
		public static SKColor Snow { get { return new SKColor(255, 255, 255); } }
		public static SKColor Mercury { get { return new SKColor(230, 230, 230); } }
		public static SKColor Silver { get { return new SKColor(204, 204, 204); } }
		public static SKColor Magnesium { get { return new SKColor(179, 179, 179); } }
		public static SKColor Aluminum { get { return new SKColor(153, 153, 153); } }
		public static SKColor Nickle { get { return new SKColor(128, 128, 128); } }
		public static SKColor Tin { get { return new SKColor(127, 127, 127); } }
		public static SKColor Steel { get { return new SKColor(102, 102, 102); } }
		public static SKColor Iron { get { return new SKColor(76, 76, 76); } }
		public static SKColor Tungsten { get { return new SKColor(51, 51, 51); } }
		public static SKColor Lead { get { return new SKColor(25, 25, 25); } }
		public static SKColor Licorice { get { return new SKColor(0, 0, 0); } }

		public static SKColor Maroon { get { return new SKColor(128, 0, 64); } }
		public static SKColor Plum { get { return new SKColor(128, 0, 128); } }
		public static SKColor Eggplant { get { return new SKColor(64, 0, 128); } }
		public static SKColor Midnight { get { return new SKColor(0, 0, 128); } }
		public static SKColor Ocean { get { return new SKColor(0, 64, 128); } }
		public static SKColor Teal { get { return new SKColor(0, 128, 128); } }
		public static SKColor Moss { get { return new SKColor(0, 128, 64); } }
		public static SKColor Clover { get { return new SKColor(0, 128, 0); } }
		public static SKColor Fern { get { return new SKColor(64, 128, 0); } }
		public static SKColor Asparagus { get { return new SKColor(128, 128, 0); } }
		public static SKColor Mocha { get { return new SKColor(128, 64, 0); } }
		public static SKColor Cayenne { get { return new SKColor(128, 0, 0); } }

		public static SKColor Strawberry { get { return new SKColor(255, 0, 128); } }
		public static SKColor Magenta { get { return new SKColor(255, 0, 255); } }
		public static SKColor Grape { get { return new SKColor(128, 0, 255); } }
		public static SKColor Blueberry { get { return new SKColor(0, 0, 255); } }
		public static SKColor Aqua { get { return new SKColor(0, 128, 255); } }
		public static SKColor Turquoise { get { return new SKColor(0, 255, 255); } }
		public static SKColor SeaFoam { get { return new SKColor(0, 255, 128); } }
		public static SKColor Spring { get { return new SKColor(0, 255, 0); } }
		public static SKColor Lime { get { return new SKColor(128, 255, 0); } }
		public static SKColor Lemon { get { return new SKColor(255, 255, 0); } }
		public static SKColor Tangerine { get { return new SKColor(255, 128, 0); } }
		public static SKColor Maraschino { get { return new SKColor(255, 0, 0); } }

		public static SKColor Carnation { get { return new SKColor(255, 111, 207); } }
		public static SKColor Bubblegum { get { return new SKColor(255, 102, 255); } }
		public static SKColor Lavender { get { return new SKColor(204, 102, 255); } }
		public static SKColor Orchid { get { return new SKColor(102, 102, 255); } }
		public static SKColor Sky { get { return new SKColor(102, 204, 255); } }
		public static SKColor Ice { get { return new SKColor(102, 255, 255); } }
		public static SKColor Spindrift { get { return new SKColor(102, 255, 204); } }
		public static SKColor Flora { get { return new SKColor(102, 255, 102); } }
		public static SKColor Honeydew { get { return new SKColor(204, 255, 102); } }
		public static SKColor Banana { get { return new SKColor(255, 255, 102); } }
		public static SKColor Cantalope { get { return new SKColor(255, 204, 102); } }
		public static SKColor Salmon { get { return new SKColor(255, 102, 102); } }
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Clones the color.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="color">Color.</param>
		public static SKColor CloneColor(SKColor color)
		{
			return new SKColor(color.Red, color.Green, color.Blue, color.Alpha);
		}
		#endregion

		#region Private Variables
		/// <summary>
		/// The <c>KiminoColor</c> this color is based off of.
		/// </summary>
		internal KimonoColor _baseColor = null;

		/// <summary>
		/// The base hue.
		/// </summary>
		internal float Hue = 0;

		/// <summary>
		/// The base saturation.
		/// </summary>
		internal float Saturation = 0;

		/// <summary>
		/// The base brightness.
		/// </summary>
		internal float Brightness = 0;

		/// <summary>
		/// The base alpha transparency value.
		/// </summary>
		internal int Alpha = 255;

		/// <summary>
		/// Should adjust hue.
		/// </summary>
		internal bool _adjustHue = false;

		/// <summary>
		/// Should adjust saturation.
		/// </summary>
		internal bool _adjustSaturation = false;

		/// <summary>
		/// Should adjust brightness.
		/// </summary>
		internal bool _adjustBrightness = false;

		/// <summary>
		/// Should adjust alpha transparency.
		/// </summary>
		internal bool _adjustAlpha = false;

		/// <summary>
		/// The hue adjustment.
		/// </summary>
		internal float _hueAdjustment = 0;

		/// <summary>
		/// The saturation adjustment.
		/// </summary>
		internal float _saturationAdjustment = 0;

		/// <summary>
		/// The brightness adjustment.
		/// </summary>
		internal float _brightnessAdjustment = 0;

		/// <summary>
		/// The alpha adjustment.
		/// </summary>
		internal int _alphaAdjustment = 255;

		/// <summary>
		/// The color value.
		/// </summary>
		internal SKColor _color = Black;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the unique identifier.
		/// </summary>
		/// <value>The unique identifier.</value>
		public string UniqueID { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Gets or sets the <c>KimonoColor</c> that this color is based off of.
		/// </summary>
		/// <value>The base color.</value>
		public KimonoColor BaseColor
		{
			get { return _baseColor; }
			set
			{
				// Release previously connected base color
				if (_baseColor != null)
				{
					_baseColor.ColorChanged -= BaseColorChanged;
				}

				// Save new base and wireup events
				_baseColor = value;
				if (_baseColor != null)
				{
					Color = _baseColor.Color;
					_baseColor.ColorChanged += BaseColorChanged;
				}

			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; } = "Color";

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:KimonoCore.KimonoColor"/> adjusts hue.
		/// </summary>
		/// <value><c>true</c> if adjusts hue; otherwise, <c>false</c>.</value>
		public bool AdjustsHue
		{
			get { return _adjustHue; }
			set
			{
				_adjustHue = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets the hue adjustment.
		/// </summary>
		/// <value>The hue adjustment.</value>
		public float HueAdjustment
		{
			get { return _hueAdjustment; }
			set
			{
				_hueAdjustment = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:KimonoCore.KimonoColor"/> adjusts saturation.
		/// </summary>
		/// <value><c>true</c> if adjusts saturation; otherwise, <c>false</c>.</value>
		public bool AdjustsSaturation
		{
			get { return _adjustSaturation; }
			set
			{
				_adjustSaturation = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets the saturation adjustment.
		/// </summary>
		/// <value>The saturation adjustment.</value>
		public float SaturationAdjustment
		{
			get { return _saturationAdjustment; }
			set
			{
				_saturationAdjustment = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:KimonoCore.KimonoColor"/> adjusts brightness.
		/// </summary>
		/// <value><c>true</c> if adjusts brightness; otherwise, <c>false</c>.</value>
		public bool AdjustsBrightness
		{
			get { return _adjustBrightness; }
			set
			{
				_adjustBrightness = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets the brightness adjustment.
		/// </summary>
		/// <value>The brightness adjustment.</value>
		public float BrightnessAdjustment
		{
			get { return _brightnessAdjustment; }
			set
			{
				_brightnessAdjustment = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:KimonoCore.KimonoColor"/> adjusts alpha.
		/// </summary>
		/// <value><c>true</c> if adjusts alpha; otherwise, <c>false</c>.</value>
		public bool AdjustsAlpha
		{
			get { return _adjustAlpha; }
			set
			{
				_adjustAlpha = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets the alpha adjustment.
		/// </summary>
		/// <value>The alpha adjustment.</value>
		public int AlphaAdjustment
		{
			get { return _alphaAdjustment; }
			set
			{
				_alphaAdjustment = value;
				MixColor();
			}
		}

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public SKColor Color
		{
			get { return _color; }
			set
			{
				// Save new values
				value.ToHsv(out Hue, out Saturation, out Brightness);
				Alpha = value.Alpha;

				// Save initial values
				if (!_adjustHue) _hueAdjustment = Hue;
				if (!_adjustSaturation) _saturationAdjustment = Saturation;
				if (!_adjustBrightness) _brightnessAdjustment = Brightness;
				if (!_adjustAlpha) _alphaAdjustment = Alpha;

				// Mix up new color based on change
				MixColor();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoColor"/> class.
		/// </summary>
		public KimonoColor()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoColor"/> class.
		/// </summary>
		/// <param name="baseColor">Base color.</param>
		public KimonoColor(KimonoColor baseColor)
		{
			// Initialize
			BaseColor = baseColor;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:KimonoCore.KimonoColor"/> class.
		/// </summary>
		/// <param name="color">Color.</param>
		public KimonoColor(SKColor color)
		{
			// Initialize
			Color = color;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Mixes the color based on the base values and any adjustment that
		/// have been applied.
		/// </summary>
		private void MixColor()
		{
			// Get color values
			var h = (AdjustsHue) ? HueAdjustment : Hue;
			var s = (AdjustsSaturation) ? SaturationAdjustment : Saturation;
			var v = (AdjustsBrightness) ? BrightnessAdjustment : Brightness;
			var a = (AdjustsAlpha) ? AlphaAdjustment : Alpha;

			// Blend new color
			_color = SKColor.FromHsv(h, s, v, (byte)a);

			// Inform caller of change
			RaiseColorChanged();
		}

		/// <summary>
		/// Handles the base color being changed.
		/// </summary>
		private void BaseColorChanged()
		{
			// Adjust this color off of base
			Color = BaseColor.Color;
		}
		#endregion

		#region Cloning
		/// <summary>
		/// Clone this instance.
		/// </summary>
		/// <returns>The clone.</returns>
		public KimonoColor Clone()
		{
			// Duplicate color
			var newColor = new KimonoColor()
			{
				UniqueID = this.UniqueID,
				_baseColor = this._baseColor,
				Name = this.Name,
				Hue = this.Hue,
				Saturation = this.Saturation,
				Brightness = this.Brightness,
				Alpha = this.Alpha,
				_adjustHue = this._adjustHue,
				_adjustSaturation = this._adjustSaturation,
				_adjustBrightness = this._adjustBrightness,
				_adjustAlpha = this._adjustAlpha,
				_hueAdjustment = this._hueAdjustment,
				_saturationAdjustment = this._saturationAdjustment,
				_brightnessAdjustment = this._brightnessAdjustment,
				_alphaAdjustment = this._alphaAdjustment,
				_color = CloneColor(this._color)
			};

			// Return new instance
			return newColor;
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when color changed.
		/// </summary>
		public event Kimono.PortfolioEventDelegate ColorChanged;

		/// <summary>
		/// Raises the color changed event.
		/// </summary>
		internal void RaiseColorChanged()
		{
			// Inform caller
			if (ColorChanged != null) ColorChanged();
		}
		#endregion
	}
}
