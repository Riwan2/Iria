using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iria
{
	public class MainGame : Game
	{
		//FIELD
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		GameState gameState;
		RenderTarget2D target;

		//CONSTRUCTOR
		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			gameState = new GameState(this);

			graphics.PreferredBackBufferWidth = Setting.FINAL_SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = Setting.FINAL_SCREEN_HEIGHT;
			graphics.IsFullScreen = Setting.IS_FULLSCREEN;
			IsMouseVisible = Setting.IS_MOUSE_VISIBLE;
		}

		//METHOD
		public void UpdateChange()
		{
			IsMouseVisible = Setting.IS_MOUSE_VISIBLE;
			graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			target = new RenderTarget2D(GraphicsDevice, Setting.SCREEN_WIDTH, Setting.SCREEN_HEIGHT);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			AssetManager.LoadContent(this);
			gameState.ChangeScene(GameState.SceneType.Gameplay);
		}

		//UPDATE DRAW
		protected override void Update(GameTime gameTime)
		{
			
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			if (gameState.CurrentScene != null) {
				InputManager.Update();
				gameState.CurrentScene.Update(gameTime);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

			//TODO: Add your drawing code here
			GraphicsDevice.SetRenderTarget(target);
			if (gameState.CurrentScene != null) {
				gameState.CurrentScene.Draw(spriteBatch);
			}

			GraphicsDevice.SetRenderTarget(null);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, 
			                  DepthStencilState.Default, RasterizerState.CullNone);
			spriteBatch.Draw(target, new Rectangle(0, 0, Setting.FINAL_SCREEN_WIDTH, Setting.FINAL_SCREEN_HEIGHT), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
