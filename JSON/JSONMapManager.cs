using System;
using System.IO;
using Newtonsoft.Json;

namespace Iria
{
	public class JSONMap
	{
		public JSONMap() { }
		public int MapWidth { get; set; }
		public int MapHeight { get; set; }

		public int[] Map { get; set; }
		public int[] CollisionMap { get; set; }
	}

	public class JSONMapManager
	{
		//FIELD
		private JSONMap mapData;
		private string filename;
		private int[,] map;
		private int[,] collisionMap;
		private int mapWidth;
		private int mapHeight;
		//private bool saving;

		//PROPERTIE
		public JSONMap MapData { get { return mapData; } }
		public int[,] Map { get { return map; } }
		public int[,] CollisionMap { get { return collisionMap; } }
		public int MapWidth { get { return mapWidth; } }
		public int MapHeight { get { return mapHeight; } }
		//public bool Saving { get { return saving; } }

		//CONSTRUCTOR
		public JSONMapManager(string pFilename)
		{
			filename = pFilename;
			LoadMap();
			//SaveMap();
		}

		//METHODE
		public void LoadMap()
		{
			mapData = JsonConvert.DeserializeObject<JSONMap>(File.ReadAllText(filename));
			mapWidth = mapData.MapWidth;
			mapHeight = mapData.MapHeight;
			map = new int[mapHeight, mapWidth];
			collisionMap = new int[mapHeight, mapWidth];

			for (int y = 0; y < MapData.MapHeight; y++) {
				for (int x = 0; x < MapData.MapWidth; x++) {
					map[y, x] = MapData.Map[x + y * MapData.MapWidth];
					collisionMap[y, x] = MapData.CollisionMap[x + y * MapData.MapWidth];
				}
			}
		}

		public void SaveMap(int[,] pMap, int[,] pCollisionMap)
		{
			mapData.Map = new int[mapData.MapWidth * mapData.MapHeight];
			mapData.CollisionMap = new int[mapData.MapWidth * mapData.MapHeight];
			for (int y = 0; y < mapData.MapHeight; y++) {
				for (int x = 0; x < mapData.MapWidth; x++) {
					mapData.Map[x + y * mapData.MapWidth] = pMap[y, x];
					mapData.CollisionMap[x + y * mapData.MapWidth] = pCollisionMap[y, x];
				}
			}

			//mapData = new JSONMap();
			//mapData.MapWidth = 30;
			//mapData.MapHeight = 50;
			//mapData.Map = new int[mapData.MapWidth * mapData.MapHeight];
			//mapData.CollisionMap = new int[mapData.MapWidth * mapData.MapHeight];
			//for (int y = 0; y < 50; y++) {
			//	for (int x = 0; x < 30; x++) {
			//		MapData.Map[x + y * 30] = 0;
			//		MapData.CollisionMap[x + y * 30] = 0;
			//	}
			//}

			File.WriteAllText(filename, JsonConvert.SerializeObject(mapData));
		}
	}
}
