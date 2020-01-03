using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public static class AssetManager
	{
		public static SpriteFont MainFont_Large { get; private set; }
		public static SpriteFont MainFont_Normal { get; private set; }
		public static SpriteFont MainFont_Mini { get; private set; }

		public static Texture2D PointTexture;
		public static Texture2D Alert;

		public static void LoadContent(MainGame pMainGame)
		{
			MainFont_Large = pMainGame.Content.Load<SpriteFont>("Font/MainFont(Large)");
			MainFont_Normal = pMainGame.Content.Load<SpriteFont>("Font/MainFont(Normal)");
			MainFont_Mini = pMainGame.Content.Load<SpriteFont>("Font/MainFont(mini)");

			PointTexture = new Texture2D(pMainGame.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			PointTexture.SetData(new[] { Color.White });
			Alert = pMainGame.Content.Load<Texture2D>("Image/Enemie/Alert");
		}
	}
}
