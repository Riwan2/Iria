using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class TileMap
	{
		//FIELD
		private Texture2D tileset;
		private Vector2 tilePos;
		private Color colorBox = new Color(255, 120, 120, 150);
		private Color colorSpawnBox = new Color(120, 255, 120, 150);

		//PROPERTIES
		public Texture2D TilesetTexture { get { return tileset; } }
		public Rectangle[] Tiles { get; private set; }

		//STATIC
		public static Map CurrentMap;
		public static int MAP_WIDTH;
		public static int MAP_HEIGHT;
		public static int TILE_WIDTH = 32;
		public static int TILE_HEIGHT = 32;
		public static int MAX_TILE = 32;

		//CONSTRUCTOR
		public TileMap(MainGame pMainGame, Texture2D pTileset)
		{
			tileset = pTileset;
			Tiles = new Rectangle[MAX_TILE];
			int column = 16;
			for (int i = 0; i < Tiles.Length; i++) {
				Tiles[i] = new Rectangle(TILE_WIDTH * (i % column),
				                         TILE_HEIGHT * (int)(i / column),
				                         TILE_WIDTH, TILE_HEIGHT);
			}
		}

		//METHODS
		public void Load(Map map)
		{
			CurrentMap = map;
			MAP_WIDTH = CurrentMap.MapWidth;
			MAP_HEIGHT = CurrentMap.MapHeight;
		}

		//DRAW
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int y = 0; y < MAP_HEIGHT; y++) {
				for (int x = 0; x < MAP_WIDTH; x++) {
					tilePos = new Vector2(x * TILE_WIDTH, y * TILE_HEIGHT);
					spriteBatch.Draw(tileset, tilePos, Tiles[CurrentMap.CurrentMap[y, x]], Color.White,
									 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
					if (Setting.HITBOX_VISIBLE) {
						if (CurrentMap.CollisionMap[y, x] == 1)
							Primitive.DrawRectangle(spriteBatch, x * TILE_WIDTH, y * TILE_HEIGHT,
													TILE_WIDTH, TILE_HEIGHT, colorBox, 0.5f);
						else if (CurrentMap.CollisionMap[y, x] == 2)
							Primitive.DrawRectangle(spriteBatch, x * TILE_WIDTH, y * TILE_HEIGHT,
													TILE_WIDTH, TILE_HEIGHT, colorSpawnBox, 0.5f);
					}
				}
			}
		}
	}
}
