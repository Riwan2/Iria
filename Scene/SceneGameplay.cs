using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class SceneGameplay : Scene
	{
		//FIELD
		private TileMap tileMap;
		private Camera camera;
		private Area currentArea;
		private Player hero;
		private GameplayHud gameplayHud;
		private LevelEditor levelEditor;
		private GameplayMenu gameplayMenu;
		private bool pause;

		//PROPERTIES

		//STATIC
		public static List<Enemie> Enemies;

		//CONSTRUCTOR
		public SceneGameplay(MainGame pMainGame) : base(pMainGame)
		{
			
		}

		//METHODS
		public void LoadArea(string Area)
		{
			if (AreaData.Data.ContainsKey(Area))
				currentArea = AreaData.Data[Area];
			tileMap.Load(currentArea.CurrentMap);
			currentArea.Load(mainGame, hero);
		}

		#region LevelEditor
		private void LevelEditorCheck()
		{
			if (levelEditor.Activated) {
				TileMap.CurrentMap.SaveMap();
				Setting.IS_MOUSE_VISIBLE = false;
			} else {
				Setting.IS_MOUSE_VISIBLE = true;
			}
			mainGame.UpdateChange();
			
			levelEditor.Activated = !levelEditor.Activated;
			foreach (Enemie enemie in Enemies) {
				enemie.Invisible = !enemie.Invisible;
			}
			hero.Invisible = !hero.Invisible;
			gameplayHud.Invisible = !gameplayHud.Invisible;
		}
		#endregion

		#region Load & Unload
		public override void Load()
		{
			base.Load();
			Sprite spriteHero = new Sprite(mainGame.Content.Load<Texture2D>("Image/Personnage/Personnage"),
										   0, 0, 8, 4);
			Sprite spriteSword = new Sprite(mainGame.Content.Load<Texture2D>("Image/Personnage/SwordSlash"),
											0, 0, 9, 1, true);
			hero = new Player(spriteHero, spriteSword);

			Enemies = new List<Enemie>();

			gameplayHud = new GameplayHud();

			Texture2D tileset = mainGame.Content.Load<Texture2D>("Image/tileset/tileset");
			tileMap = new TileMap(mainGame, tileset);
			LoadArea("SunnyField");

			levelEditor = new LevelEditor(tileset);
			camera = new Camera(hero.Position.X - 50, 300);
		}

		public override void Unload()
		{
			base.Unload();
		}
		#endregion

		//UPDATE DRAW
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!pause) {
				if (InputManager.JustPressed(InputManager.eAction.HitBox)) Setting.HITBOX_VISIBLE = !Setting.HITBOX_VISIBLE;

				hero.Update(gameTime);
				if (!levelEditor.Activated) camera.UpdateCamera(mainGame.GraphicsDevice.Viewport, hero.Position, gameTime);
				gameplayHud.Update(gameTime, hero, camera.WorldPos);

				//Enemies
				foreach (Enemie enemie in Enemies) {
					if (!enemie.Invisible) {
						enemie.UpdateEnemie(gameTime, hero);
						enemie.Update(gameTime);
					}
				}
				Enemies.RemoveAll((obj) => obj.IsDead);

				//Area
				currentArea.Update(gameTime, hero);

				if (InputManager.JustPressed(InputManager.eAction.MapEditor)) LevelEditorCheck();
				if (levelEditor.Activated) levelEditor.Update(gameTime, camera);

				if (InputManager.JustPressed(InputManager.eAction.Confirm)) {
					TileMap.CurrentMap.SaveMap();
					currentArea.Load(mainGame, hero);
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, 
			                  DepthStencilState.Default, RasterizerState.CullNone, null, camera.CameraMatrix);

			base.Draw(spriteBatch);
			hero.Draw(spriteBatch);

			//Enemie
			foreach (Enemie enemie in Enemies) {
				if (!enemie.Invisible) enemie.Draw(spriteBatch);
			}

			tileMap.Draw(spriteBatch);
			gameplayHud.Draw(spriteBatch);

			if (levelEditor.Activated) levelEditor.Draw(spriteBatch);

			spriteBatch.End();
		}
	}
}
