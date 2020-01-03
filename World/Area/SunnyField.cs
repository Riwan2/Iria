using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class SunnyField : Area
	{
		//FIELD
		private const int NB_ENEMIE = 80;
		private Timer respawnTimer;
		private int nbSpawnEnemie;
		private Texture2D slimeTexture;

		//PROPERTIES

		//CONSTRUCTOR
		public SunnyField() : base ("SunnyField", NB_ENEMIE)
		{
			map = new Map("SunnyField");
			respawnTimer = new Timer(20000);
		}

		//METHOD
		public override void Load(MainGame pMainGame, Player hero)
		{
			Spawn(hero, 11, 40);
			base.Load(pMainGame, hero);
			SceneGameplay.Enemies.Clear();

			//Enemie
			slimeTexture = pMainGame.Content.Load<Texture2D>("Image/Enemie/Slime");
			Spawn(NB_ENEMIE);
		}

		#region Respawn & Spawn
		private void Spawn(int pNbEnemie)
		{
			for (int i = 0; i < pNbEnemie; i++) {
				Enemie slime = new Slime(new Sprite(slimeTexture, 0, 0,
												   5, 1, true));
				Spawn(slime, enemieLocation[i].X, enemieLocation[i].Y);
				SceneGameplay.Enemies.Add(slime);
			}
		}

		private void RespawnCheck(GameTime gameTime, Player hero)
		{
			if (respawnTimer.Update(gameTime)) {
				if (currentNbEnemie < nbEnemie) {
					nbSpawnEnemie = (int)Math.Ceiling((nbEnemie - currentNbEnemie) * 0.3f);
					currentNbEnemie += nbSpawnEnemie;
					ChooseEnemieLocation(nbSpawnEnemie, hero);
					Spawn(nbSpawnEnemie);
					Console.WriteLine("Respawn de : " + nbSpawnEnemie + " slime !");
				}
			}
		}
		#endregion

		//UPDATE & DRAW
		public override void Update(GameTime gameTime, Player hero)
		{
			base.Update(gameTime, hero);

			RespawnCheck(gameTime, hero);
		}
	}
}
