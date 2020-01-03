using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class GameplayHud
	{
		//FIELD
		private Text[] lst_text;
		private bool isInvisible;

		//PROPERTIES
		public bool Invisible { get { return isInvisible; } set { isInvisible = value; } }

		//CONSTRUCTOR
		public GameplayHud()
		{
			lst_text = new Text[1];
			lst_text[0] = new Text(new Vector2(0.01f, -0.01f), "Life : 0", 1f, Color.White,
								   AssetManager.MainFont_Normal, false, true);
		}

		//METHOD

		//UPDATE & DRAW
		public void Update(GameTime gameTime, Player hero, Point cameraPosition)
		{
			if (!Invisible) {
				lst_text[0].CurrentText = "Life : " + hero.Life.ToString();

				for (int i = 0; i < lst_text.Length; i++) {
					lst_text[i].Offset = cameraPosition.ToVector2();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!Invisible) {
				for (int i = 0; i < lst_text.Length; i++) {
					lst_text[i].Draw(spriteBatch);
				}
			}
		}
	}
}
