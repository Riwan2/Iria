using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	//DELEGATE
	public delegate void OnPress(Button pButton);

	public class Button
	{
		//FIELD
		private Text currentText;
		private bool selected;
		public OnPress onPress;

		//PROPERTIES
		public bool Selected { get { return selected; } }
		public Vector2 Position
		{
			get { return currentText.Position; }
			set { currentText.Position = value; }
		}

		//CONSTRUCTOR
		public Button(Vector2 pPosition, string pText, int pScale, Color pColor, SpriteFont pFont, 
		              bool center = false, bool floatCoord = false)
		{
			currentText = new Text(pPosition, pText, pScale, pColor, pFont, center, floatCoord);
		}

		//METHODS
		public void Select()
		{
			selected = !selected;
		}

		//UPDATE & DRAW
		public void Update(GameTime gameTime)
		{
			currentText.Colors = Color.White;
			if (Selected) {
				currentText.Colors = Color.Gold;
				if (InputManager.JustPressed(InputManager.eAction.Confirm)) {
					onPress(this);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			currentText.Draw(spriteBatch);
		}
	}
}
