using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Entity
	{
		//FIELD
		protected Sprite sprite;
		protected Rectangle hitBox;
		protected bool inMovement;
		private Color colorBox = new Color(120, 255, 255, 50);
		private bool isInvisible;

		//PROPERTIES
		public Point Position { 
			get 
			{
				return new Point(hitBox.X + hitBox.Width / 2,
								 hitBox.Y + hitBox.Height / 2); 
			} 
			set
			{
				hitBox.X = value.X;
				hitBox.Y = value.Y;
			}
		}
		public int Width { get { return sprite.Width; } }
		public int Height { get { return sprite.Height; } }
		public Rectangle HitBox { get { return hitBox; } }
		public bool Invisible { get { return isInvisible; } set { isInvisible = value; } }

		//CONSTRUCTOR
		public Entity(Sprite pSprite, int x = 0, int y = 0)
		{
			sprite = pSprite;
			hitBox = new Rectangle(x, y, sprite.Width, sprite.Height);
		}

		//METHODS
		protected void Move(float pX, float pY)
		{
			hitBox = new Rectangle((int)(hitBox.X + pX), (int)(hitBox.Y + pY),
								   sprite.Width, sprite.Height);
			inMovement = true;
		}

		//UPDATE & DRAW
		public virtual void Update(GameTime gameTime)
		{
			inMovement = false;
			sprite.SetPosition(hitBox.X + (int)sprite.Origin.X, hitBox.Y + (int)sprite.Origin.Y);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			sprite.Draw(spriteBatch);
			if (Setting.HITBOX_VISIBLE)
				Primitive.DrawRectangle(spriteBatch, hitBox, colorBox);
		}
	}
}
