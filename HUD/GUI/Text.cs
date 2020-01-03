using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Text
	{
		//FIELD
		private string currentText;
		private SpriteFont font;
		private Vector2 position;
		private int width;
		private int height;
		private Color color;
		private float scale;
		private Vector2 origin;
		private SpriteEffects spriteEffect;
		private float layerDepth;
		private float rotation;
		private Vector2 offset;

		private bool floatCoordinate = false;
		private Vector2 finalPosition;

		//PROPERTIES
		public Color Colors 
		{ 
			get { return color; }
			set { color = value; }
		}
		public string CurrentText 
		{ 
			get { return currentText; } 
			set { currentText = value; }
		}
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}
		public float Scale { get { return scale; } }
		public Vector2 Offset { 
			get { return offset; } 
			set { offset = value; }
		}

		//CONSTRUCTOR
		public Text(Vector2 pPosition, string pText, float pScale, Color pColor, SpriteFont pFont, 
		            bool center = false, bool pFloatCoord = false)
		{
			floatCoordinate = pFloatCoord;
			position = pPosition;
			currentText = pText;
			scale = pScale;
			color = pColor;
			font = pFont;
			spriteEffect = SpriteEffects.None;
			rotation = 0;
			layerDepth = 0f;
			offset = Vector2.Zero;

			Vector2 size = font.MeasureString(currentText);
			width = (int)(size.X * scale);
			height = (int)(size.Y * scale);

			if (center) origin = new Vector2(size.X / 2, size.Y / 2);
			else origin = new Vector2(0, 0);
		}

		//UPDATE & DRAW
		public void Draw(SpriteBatch spriteBatch)
		{
			if (floatCoordinate) finalPosition = util.getPos(position.X, position.Y);
			else finalPosition = Position;

			finalPosition += offset;
			spriteBatch.DrawString(font, currentText, finalPosition, color, rotation, origin,
								  scale, spriteEffect, layerDepth);
		}
	}
}
