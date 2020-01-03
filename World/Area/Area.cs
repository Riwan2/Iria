using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class Area
	{
		//FIELDS
		protected string name;
		protected Map map;
		protected List<Point> enemieLocation;
		protected int currentNbEnemie;
		protected int nbEnemie;
		private int nbLocationChoosed;
		private int random;
		private int nbTry;
		private Point point;
		private bool noSpawn;

		//PROPERTIES
		public Map CurrentMap { get { return map; } }

		//CONSTRUCTOR
		public Area(string pName, int pNbEnemie) 
		{
			name = pName;
			nbEnemie = pNbEnemie;
		}

		//METHOD
		public virtual void Load(MainGame pMainGame, Player hero)
		{
			enemieLocation = new List<Point>();
			nbTry = 0;
			ChooseEnemieLocation(nbEnemie, hero);
		}

		#region Spawn
		public void Spawn(LivingEntity entity, int tileX, int tileY)
		{
			entity.Position = new Point(tileX * TileMap.TILE_WIDTH,
										tileY * TileMap.TILE_HEIGHT);
		}
		#endregion

		#region Enemie Location
		protected void ChooseEnemieLocation(int pNbEnemie, Player hero)
		{
			enemieLocation.Clear();
			nbLocationChoosed = 0;
			while (nbLocationChoosed < pNbEnemie) {
				nbTry++;
				for (int y = 0; y < CurrentMap.MapHeight; y++) {
					for (int x = 0; x < CurrentMap.MapWidth; x++) {
						random = util.getInt(0, 100);
						point = new Point(x, y);
						noSpawn = false;
						if (CurrentMap.CollisionMap[y, x] == 2 && random == 0) {
							if (nbTry < 500) {
								foreach (Point item in enemieLocation) {
									if (point == item || util.getHeuristic(point,util.getTile(hero.Position.X,hero.Position.Y)) < 5) {
										noSpawn = true;
										break;
									}
								}
								foreach (Enemie enemie in SceneGameplay.Enemies) {
									if (util.getHeuristic(point, util.getTile(enemie.Position.X, enemie.Position.Y)) < 1) {
										noSpawn = true;
									}
								}
							}
							if (!noSpawn) {
								enemieLocation.Add(point);
								nbLocationChoosed++;
							}
						}

					}
				}
			}
			Console.WriteLine("Nombre de try pour spawn : " + nbTry);
		}
		#endregion

		//UPDATE & DRAW
		public virtual void Update(GameTime gameTime, Player hero)
		{
			currentNbEnemie = SceneGameplay.Enemies.Count;
		}
	}

	public static class AreaData
	{
		public static Dictionary<string, Area> Data = new Dictionary<string, Area>()
		{
			{ "SunnyField", new SunnyField() },
		};
	}
}
