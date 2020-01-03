using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class Map
	{
		//FIELD
		private int[,] map;
		private int[,] collisionMap;
		private int mapWidth;
		private int mapHeight;
		private Point spawnPoint;
		private JSONMapManager jsonMapManager;

		//PROPERTIES
		public int[,] CurrentMap { get { return map; } set { map = value; } }
		public int[,] CollisionMap { get { return collisionMap; } set { collisionMap = value; } }
		public int MapWidth { get { return mapWidth; } }
		public int MapHeight { get { return mapHeight; } }
		public Point SpawnPoint { get { return spawnPoint; } }

		//CONSTRUCTOR
		public Map(string pFilename)
		{
			jsonMapManager = new JSONMapManager("Map/" + pFilename + ".json");
			LoadMap();

			//Generate();
			//CollisionGeneration();
		}

		//METHOD
		#region Load
		public void LoadMap()
		{
			mapWidth = jsonMapManager.MapData.MapWidth;
			mapHeight = jsonMapManager.MapData.MapHeight;
			map = jsonMapManager.Map;
			collisionMap = jsonMapManager.CollisionMap;
		}
		#endregion

		#region Save
		public void SaveMap()
		{
			jsonMapManager.SaveMap(map, collisionMap);
		}
		#endregion

		#region Generate
		private void CollisionGeneration()
		{
			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					if (x == 0 || y == 0 || x == mapWidth-1 || y == mapHeight-1) CollisionMap[y, x] = 1;
					else CollisionMap[y, x] = 0;
				}
			}
		}

		private void Generate()
		{
			for (int y = 0; y < mapHeight; y++) {
				for (int x = 0; x < mapWidth; x++) {
					int Rand = util.getInt(0, 100);
					if (Rand < 60) {
						map[y, x] = 0;
					} else if (Rand < 80) {
						map[y, x] = 1;
					} else if (Rand < 95) {
						map[y, x] = 2;
					} else if (Rand < 97) {
						map[y, x] = 3;
					} else {
						map[y, x] = 4;
					}
				}
			}
		}
		#endregion
	}
}
