using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Scene
	{
		protected MainGame mainGame;

		public Scene(MainGame pMainGame)
		{
			mainGame = pMainGame;
		}

		public virtual void Unload()
		{

		}

		public virtual void Load()
		{

		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{

		}
	}
}
