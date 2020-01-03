using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class util
	{
		static Random RandomGen = new Random();

		//Seed
		public static void setRandomSedd(int pSeed)
		{
			RandomGen = new Random(pSeed);
		}

		public static int getInt(int pMin, int pMax)
		{
			return RandomGen.Next(pMin, pMax + 1);
		}

		public static double getDouble()
		{
			return RandomGen.NextDouble();
		}

		public static float getAngle(Point a, Point b)
		{
			float xDiff = a.X - b.X;
			float yDiff = a.Y - b.Y;
			return (float)Math.Atan2(yDiff, xDiff);
		}

		public static int getDistance(Point a, Point b)
		{
			int d = (int)((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
			return (int)(Math.Sqrt(d));
		}

		public static int getHeuristic(Point a, Point b)
		{
			//Manhattan distance on a square grid
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
		}

		public static Point getTile(int pX, int pY)
		{
			return new Point((int)(pX / TileMap.TILE_WIDTH),
			                 (int)(pY / TileMap.TILE_HEIGHT));
		}

		public static Point getWorldPos(int pX, int pY)
		{
			return new Point(pX * TileMap.TILE_WIDTH + TileMap.TILE_WIDTH / 2,
			                 pY * TileMap.TILE_HEIGHT + TileMap.TILE_HEIGHT / 2);
		}

		public static Vector2 getPos(float pX, float pY)
		{
			return new Vector2(Setting.SCREEN_WIDTH * pX,
			                   Setting.SCREEN_HEIGHT * pY);
		}
	}
}
