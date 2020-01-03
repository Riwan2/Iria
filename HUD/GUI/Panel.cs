using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Panel
	{
		//FIELD
		protected Text title;
		protected Button[] lstButton;
		protected int buttonIndex;

		//PROPERTIES
		public int ButtonIndex { get { return buttonIndex; } }

		//CONSTRUCTOR
		public Panel()
		{
			buttonIndex = 0;
		}

		//METHODS
		public void Init()
		{
			lstButton[ButtonIndex].Select();
		}

		public void SetButton(int pIndex)
		{
			lstButton[ButtonIndex].Select();
			buttonIndex = pIndex;
			lstButton[ButtonIndex].Select();
		}

		#region Button Selection
		protected void BaseButtonUpdate()
		{
			if ((InputManager.JustPressed(InputManager.eAction.MoveDown) ||
				InputManager.JustPressed(InputManager.eAction.Down)) && ButtonIndex < lstButton.Length - 1) {
				buttonIndex++;
				UpdateSelection(ButtonIndex, true);
			} else if ((InputManager.JustPressed(InputManager.eAction.MoveUp) ||
						InputManager.JustPressed(InputManager.eAction.Up)) && ButtonIndex > 0) {
				buttonIndex--;
				UpdateSelection(ButtonIndex, false);
			}
		}

		protected void UpdateSelection(int pIndex, bool pAncient) //True : - | False : +
		{
			int ancient = pIndex;
			if (pAncient) ancient--; else ancient++;

			lstButton[ancient].Select();
			lstButton[pIndex].Select();
		}
		#endregion

		//UPDATE & DRAW
		public virtual void Update(GameTime gameTime)
		{
			//Update button
			for (int i = 0; i < lstButton.Length; i++) {
				if (lstButton[i] != null) {
					lstButton[i].Update(gameTime);
				}
			}
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			//Title
			title.Draw(spriteBatch);
			//Draw button
			for (int i = 0; i < lstButton.Length; i++) {
				if (lstButton[i] != null) {
					lstButton[i].Draw(spriteBatch);
				}
			}
		}
	}
}
