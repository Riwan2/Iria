using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class LevelEditor
	{
		//FIELD
		private bool activated;
		private Rectangle tileRectangle;
		private Color tileRectangleColor = new Color(255, 230, 230, 195);
		private Point tilePos;
		private Point mousePos;
		private TileSelector tileSelector;
		private float cameraSpeed;

		//PROPERTIES
		public bool Activated { get { return activated; } set { activated = value; } }

		//CONSTRUCTOR
		public LevelEditor(Texture2D pTileset)
		{
			tileSelector = new TileSelector(pTileset, 10, 10);
		}

		//METHODS
		private void PlaceTile()
		{
			if (tilePos.X >= 0 && tilePos.X < TileMap.MAP_WIDTH && tilePos.Y >= 0 && tilePos.Y < TileMap.MAP_HEIGHT) {
				if (!Setting.HITBOX_VISIBLE)
					TileMap.CurrentMap.CurrentMap[tilePos.Y, tilePos.X] = tileSelector.CurrentTileIndex;
				else {
					if (tileSelector.CurrentTileIndex == 0)
						TileMap.CurrentMap.CollisionMap[tilePos.Y, tilePos.X] = 1;
					else
						TileMap.CurrentMap.CollisionMap[tilePos.Y, tilePos.X] = 2;
				}
			}
		}

		private void EraseTile()
		{
			if (tilePos.X >= 0 && tilePos.X < TileMap.MAP_WIDTH && tilePos.Y >= 0 && tilePos.Y < TileMap.MAP_HEIGHT) {
				if (!Setting.HITBOX_VISIBLE)
					TileMap.CurrentMap.CurrentMap[tilePos.Y, tilePos.X] = 0;
				else 
					TileMap.CurrentMap.CollisionMap[tilePos.Y, tilePos.X] = 0;
			}
		}

		//UPDATE & DRAW
		public void Update(GameTime gameTime, Camera camera)
		{
			mousePos = new Point((int)(InputManager.MousePos.X / camera.Zoom) + (int)(camera.WorldPos.X * camera.Zoom),
			                     (int)(InputManager.MousePos.Y / camera.Zoom) + (int)(camera.WorldPos.Y * camera.Zoom));
			tilePos = util.getTile(mousePos.X, mousePos.Y);
			tileRectangle = new Rectangle(tilePos.X * TileMap.TILE_WIDTH,
			                              tilePos.Y * TileMap.TILE_HEIGHT,
			                              TileMap.TILE_WIDTH, TileMap.TILE_HEIGHT);
			tileSelector.Update(gameTime, camera);

			if (InputManager.isClicked(InputManager.eMouse.LeftClick) ||
				InputManager.isClickHeld(InputManager.eMouse.LeftClick)) {
				PlaceTile();
			} else if (InputManager.isClicked(InputManager.eMouse.RightClick) ||
			           InputManager.isClickHeld(InputManager.eMouse.RightClick)) {
				EraseTile();
			}

			cameraSpeed = 3f / camera.Zoom;
			if (InputManager.JustPressed(InputManager.eAction.A)) {
				if (camera.Zoom >= 0.25f) {
					camera.Zoom -= 0.1f;
					camera.UpdateMatrix();
				}
			} else if (InputManager.JustPressed(InputManager.eAction.E)) {
				if (camera.Zoom < 1f) {
					camera.Zoom += 0.1f;
					camera.UpdateMatrix();
				}
			}
			if (InputManager.isHeld(InputManager.eAction.MoveUp)) camera.MoveUp(cameraSpeed, false);
			else if (InputManager.isHeld(InputManager.eAction.MoveDown)) camera.MoveDown(cameraSpeed, false);
			if (InputManager.isHeld(InputManager.eAction.MoveRight)) camera.MoveRight(cameraSpeed, false);
			else if (InputManager.isHeld(InputManager.eAction.MoveLeft)) camera.MoveLeft(cameraSpeed, false);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Primitive.DrawRectangle(spriteBatch, tileRectangle, tileRectangleColor);
			tileSelector.Draw(spriteBatch);
		}
	}
}
