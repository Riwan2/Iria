using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class CollisionComponent
	{
		//FIELD
		private Point up;
		private Point down;
		private Point left;
		private Point right;
		private Point point;
		private int offsetWidth = -2;

		//PROPERTIES
		public Rectangle CollisionBox { get; private set; }

		//CONSTRUCTOR
		public CollisionComponent(Rectangle pCollisionBox)
		{
			CollisionBox = pCollisionBox;
		}

		//METHOD
		public void UpdateCollisionBox(Rectangle pCollisionBox)
		{
			CollisionBox = pCollisionBox;
		}

		#region MapCollision
		public bool CheckMapCollision(Point pPoint)
		{
			point = util.getTile(pPoint.X, pPoint.Y);
			if (TileMap.CurrentMap.CollisionMap[point.Y, point.X] == 1) {
				return true;
			}
			return false;
		}
		public bool CheckMapRightCollision(float Speed)
		{
			CollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width-1, CollisionBox.Height);
			up = new Point((int)(CollisionBox.X + CollisionBox.Width + Speed), CollisionBox.Y);
			down = new Point((int)(CollisionBox.X + CollisionBox.Width + Speed), CollisionBox.Y + CollisionBox.Height);
			if (CheckMapCollision(up) || CheckMapCollision(down)) return true;
			return false;
		}
		public bool CheckMapLeftCollision(float Speed)
		{
			CollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
			up = new Point((int)(CollisionBox.X - Speed), CollisionBox.Y);
			down = new Point((int)(CollisionBox.X - Speed), CollisionBox.Y + CollisionBox.Height);
			if (CheckMapCollision(up) || CheckMapCollision(down)) return true;
			return false;
		}
		public bool CheckMapUpCollision(float Speed)
		{
			CollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width + offsetWidth, CollisionBox.Height);
			right = new Point(CollisionBox.X, (int)(CollisionBox.Y - Speed));
			left = new Point(CollisionBox.X + CollisionBox.Width, (int)(CollisionBox.Y - Speed));
			if (CheckMapCollision(right) || CheckMapCollision(left)) return true;
			return false;
		}
		public bool CheckMapDownCollision(float Speed)
		{
			CollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width + offsetWidth, CollisionBox.Height);
			right = new Point(CollisionBox.X, (int)(CollisionBox.Y + CollisionBox.Height + Speed));
			left = new Point(CollisionBox.X + CollisionBox.Width, (int)(CollisionBox.Y + CollisionBox.Height + Speed));
			if (CheckMapCollision(right) || CheckMapCollision(left)) return true;
			return false;
		}
		#endregion
	}
}
