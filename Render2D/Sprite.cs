using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Sprite
	{
		//FIELD
		private Texture2D texture;
		private Rectangle destinationRectangle;
		private Rectangle sourceRectangle;
		private Vector2 origin;
		private float rotation;
		private Color color;
		private SpriteEffects spriteEffect;
		private float layerDepth;
		private int column;
		private int line;
		private int width;
		private int height;

		//PROPERTIES
		public float Rotation  {
			get { return rotation; }
			set { rotation = value; }
		}
		public Color Color {
			get { return color; }
			set { color = value; }
		}
		public Vector2 Origin {	
			get { return origin; } 
			set { origin = value; } 
		}
		public SpriteEffects SpriteEffects
		{
			get { return spriteEffect; }
			set { spriteEffect = value; }
		}
		public Rectangle DestinationRectangle
		{
			get { return destinationRectangle; }
			set { destinationRectangle = value; }
		}
		public float Layer
		{
			get { return layerDepth; }
			set { layerDepth = value; }
		}
		public int Width { get { return width; } }
		public int Height { get { return height; } }
		public int MaxIndex { get { return column * line; } }
		public Point Position { get { return new Point(destinationRectangle.X, destinationRectangle.Y); } }

		//CONSTRUCTOR
		public Sprite(Texture2D pTexture, int posX = 0, int posY = 0, int pColumn = 1, int pLine = 1, bool center = false)
		{
			texture = pTexture;
			rotation = 0;
			color = Color.White;
			spriteEffect = SpriteEffects.None;
			layerDepth = 0f;
			column = pColumn;
			line = pLine;
			width = texture.Width / column;
			height = texture.Height / line;
			destinationRectangle = new Rectangle(posX, posY, width, height);
			sourceRectangle = new Rectangle(0, 0, width, height);
			if (center) origin = new Vector2(width / 2, height / 2);
			else origin = new Vector2(0, 0);
		}

		//METHODE
		public void SetSourceRectangle(int x, int y)
		{
			sourceRectangle = new Rectangle(x * width, y * height, width, height);
		}
		public void SetPosition(int pX, int pY)
		{
			destinationRectangle = new Rectangle(pX, pY, destinationRectangle.Width, destinationRectangle.Height);
		}

		//UPDATE DRAW
		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, 
			                 rotation, origin, spriteEffect, layerDepth);
		}
	}
}
