using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class TileSelector
	{
		//FIELD
		private int currentTileIndex;
		private int currentLine;
		private int currentColumn;
		private Texture2D tileset;
		private Point position;
		private Vector2 currentPosition;
		private Rectangle tileRectangle;
		private Color tileRectangleColor = new Color(120, 120, 255, 230);
		private int tileRectangleDif = 2;
		private Rectangle tileSelected;
		private Color tileSelectedColor = new Color(255, 0, 255, 50);
		private bool invisible;
		private Rectangle collisionBox;
		private Rectangle spawnBox;
		private Color collisionBoxColor = new Color(255, 120, 120);
		private Color spawnBoxColor = new Color(120, 255, 120);
		private int maxIndex;

		//PROPERTIE
		public int CurrentTileIndex { get { return currentTileIndex; } }
		public bool Invisible { get { return invisible; } set { invisible = value; } }

		//CONSTRUCTOR
		public TileSelector(Texture2D pTileset, int pX, int pY)
		{
			currentTileIndex = 0;
			tileset = pTileset;
			position = new Point(pX, pY);
			currentColumn = 0;
			currentLine = 0;
		}

		//METHOD
		#region Selection Movement
		private void Movement()
		{
			if (InputManager.MouseWheelLeft) {
				if (currentTileIndex > 0) {
					currentTileIndex--;
				}
			} else if (InputManager.MouseWheelRight) {
				if (currentTileIndex < maxIndex - 1) {
					currentTileIndex++;
				}
			}

			currentColumn = currentTileIndex % 16;
			currentLine = (int)(currentTileIndex / 16);
		}
		#endregion

		#region MAP Layer
		private void Layer1()
		{
			maxIndex = TileMap.MAX_TILE;
			tileRectangle = new Rectangle((int)currentPosition.X - tileRectangleDif,
										  (int)currentPosition.Y - tileRectangleDif,
										  tileset.Width + tileRectangleDif * 2,
										  tileset.Height + tileRectangleDif * 2);
		}
		private void Layer2()
		{
			if (currentTileIndex > maxIndex) currentTileIndex = 0;
			maxIndex = 2;
			tileRectangle = new Rectangle((int)currentPosition.X - tileRectangleDif,
										  (int)currentPosition.Y - tileRectangleDif,
										  TileMap.TILE_WIDTH * 2 + tileRectangleDif * 2,
										  TileMap.TILE_HEIGHT + tileRectangleDif * 2);
			collisionBox = new Rectangle((int)currentPosition.X, (int)currentPosition.Y,
										 TileMap.TILE_WIDTH, TileMap.TILE_HEIGHT);
			spawnBox = new Rectangle((int)currentPosition.X + TileMap.TILE_WIDTH,  (int)currentPosition.Y,
			                         TileMap.TILE_WIDTH, TileMap.TILE_HEIGHT);
		}
		#endregion

		//UPDATE & DRAW
		public void Update(GameTime gameTime, Camera camera)
		{
			Movement();
			currentPosition = new Vector2(position.X + camera.WorldPos.X * camera.Zoom,
			                              position.Y + camera.WorldPos.Y * camera.Zoom);
			tileSelected = new Rectangle((int)(currentPosition.X + currentColumn * TileMap.TILE_WIDTH),
			                             (int)(currentPosition.Y + currentLine * TileMap.TILE_HEIGHT), 
			                             TileMap.TILE_WIDTH, 
			                             TileMap.TILE_HEIGHT);
			if (InputManager.JustPressed(InputManager.eAction.Tab))
				invisible = !invisible;

			if (!Setting.HITBOX_VISIBLE) Layer1();
			else Layer2();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!invisible) {
				Primitive.DrawRectangle(spriteBatch, tileRectangle, tileRectangleColor, 0.1f);
				if (!Setting.HITBOX_VISIBLE) {
					spriteBatch.Draw(tileset, currentPosition, Color.White);
				} else {
					Primitive.DrawRectangle(spriteBatch, collisionBox, Color.Red);
					Primitive.DrawRectangle(spriteBatch, spawnBox, spawnBoxColor);
				}
				Primitive.DrawRectangle(spriteBatch, tileSelected, tileSelectedColor);
			}
		}
	}
}
