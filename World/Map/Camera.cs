using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Camera
	{
		//FIELD
		private Vector2 position;

		//PROPERTIES
		public Matrix CameraMatrix { get; protected set; }
		public Vector2 Position { get { return position; } }
		public Point WorldPos 
		{	
			get  { return new Point((int)(position.X / Zoom - Setting.SCREEN_WIDTH / 2 / Zoom),
			                       (int)(position.Y / Zoom - Setting.SCREEN_HEIGHT / 2 / Zoom));} 
		}
		public float Zoom { get; set; } = 1f;

		//CONSTRUCTOR
		public Camera(int pX, int pY)
		{
			position = new Vector2(pX, pY);
		}

		//METHOD
		#region Movement
		public void MoveCamera(float pX, float pY)
		{
			position = new Vector2((int)(position.X + pX),(int)(position.Y + pY));
		}

		public void UpdateMatrix()
		{
			CameraMatrix = Matrix.CreateTranslation(new Vector3(-(position.X),
																-(position.Y), 0)) *
								 Matrix.CreateTranslation(new Vector3(Setting.SCREEN_WIDTH / 2,
																	  Setting.SCREEN_HEIGHT / 2, 0)) *
								 Matrix.CreateScale(Zoom);
		}
		public void MoveRight(float Amount, bool collision = true)
		{
			if (!CollisionRight() || !collision) MoveCamera(Amount, 0);
			UpdateMatrix();
		}
		public void MoveLeft(float Amount, bool collision = true)
		{
			if (!CollisionLeft() || !collision) MoveCamera(-Amount, 0);
			UpdateMatrix();
		}
		public void MoveDown(float Amount, bool collision = true)
		{
			if (!CollisionDown() || !collision) MoveCamera(0, Amount);
			UpdateMatrix();
		}
		public void MoveUp(float Amount, bool collision = true)
		{
			if (!CollisionUp() || !collision) MoveCamera(0, -Amount);
			UpdateMatrix();
		}
		#endregion

		#region Colision
		private bool CollisionRight()
		{
			if (position.X + Setting.SCREEN_WIDTH / 2 >= TileMap.MAP_WIDTH * TileMap.TILE_WIDTH) return true;
			return false;
		}
		private bool CollisionLeft()
		{
			if (position.X - Setting.SCREEN_WIDTH / 2 <= 0) return true;
			return false;
		}
		private bool CollisionDown()
		{
			if (position.Y + Setting.SCREEN_HEIGHT / 2 >= TileMap.MAP_HEIGHT * TileMap.TILE_HEIGHT) return true;
			return false;
		}private bool CollisionUp()
		{
			if (position.Y - Setting.SCREEN_HEIGHT / 2 <= 0) return true;
			return false;
		}
		#endregion

		//UPDATE
		public void UpdateCamera(Viewport bounds, Point PlayerPosition, GameTime gameTime)
		{
			int difX = (int)(PlayerPosition.X - position.X);
			int difY = (int)(PlayerPosition.Y - position.Y);

			if (Math.Abs(difX) > 1) {
				if (difX < 0)
					MoveLeft(Math.Abs(difX / 20));
				else if (difX > 0)
					MoveRight(Math.Abs(difX / 20));
			}
			if (Math.Abs(difY) > 1) {
				if (difY < 0)
					MoveUp(Math.Abs(difY / 20));
				else if (difY > 0)
					MoveDown(Math.Abs(difY / 20));
			}
		}
	}
}
