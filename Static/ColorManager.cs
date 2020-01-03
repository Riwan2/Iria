using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public static class ColorManager
	{
		//METHODs
		public static Color Inverse(Color color)
		{
			return new Color(255 - color.R, 255 - color.G, color.B, color.A);
		}

		#region Add & Substract
		public static Color Add(Color color1, Color color2, bool Alpha = true)
		{
			byte alpha = (Alpha) ? (byte)Math.Min(color1.A + color2.A, 255) : color1.A;

			return new Color(
				(byte)Math.Min(color1.R + color2.R, 255),
				(byte)Math.Min(color1.G + color2.G, 255),
				(byte)Math.Min(color1.B + color2.B, 255),
				alpha
				);
		}

		public static Color Subtract(Color color1, Color color2, bool Alpha = true)
		{
			byte alpha = (Alpha) ? (byte)Math.Max(color1.A - color2.A, 0) : color1.A;

			return new Color(
				(byte)Math.Max(color1.R - color2.R, 0),
				(byte)Math.Max(color1.G - color2.G, 0),
				(byte)Math.Max(color1.B - color2.B, 0),
				alpha
				);
		}
		#endregion

		#region Multiply & Divide
		public static Color Multiply(Color color, float value, bool Alpha = true)
		{
			byte alpha = (Alpha) ? (byte)Math.Min(color.A * value, 255) : color.A;

			return new Color(
				(byte)Math.Min(color.R * value, 255),
				(byte)Math.Min(color.G * value, 255),
				(byte)Math.Min(color.B * value, 255),
				alpha
				);
		}

		public static Color Divide(Color color, float value, bool Alpha = true)
		{
			byte alpha = (Alpha) ? (byte)(color.A / value) : color.A;

			return new Color(
				(byte)(color.R / value),
				(byte)(color.G / value),
				(byte)(color.B / value),
				alpha);
		}
		#endregion
	}
}
