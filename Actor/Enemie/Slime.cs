using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class Slime : Enemie
	{
		//FIELD
		private Timer animationTimer;
		private int index;
		private float angle;
		private float jumpPower;
		private float currentJumpPower;
		private Timer chooseLocationTimer;
		private bool locationChoosed;

		//PROPERTIES

		//CONSTRUCTOR
		public Slime(Sprite pSprite) : base(pSprite, 1, 40, 5, 150)
		{
			animationTimer = new Timer(200);
			index = util.getInt(0, sprite.MaxIndex - 1);
			jumpPower = 8f;
			currentJumpPower = 0;
			currentAI = new SlimeAI(500, 800, 2000);
			chooseLocationTimer = new Timer(500);
		}
		//METHODS
		#region Animation
		private void UpdateAnimation(GameTime gameTime)
		{
			if (animationTimer.Update(gameTime)) {
				if (index < sprite.MaxIndex - 1) index++;
				else index = 0;
			}
			sprite.SetSourceRectangle(index, 0);
		}
		#endregion

		#region AI 
		public void CheckAI(GameTime gameTime, Player hero)
		{
			freezeAI = false;

			if (currentAI.CurrentState == State.PrepareToCharge) {
				freezeAI = true;
				if (!locationChoosed) {
					if (chooseLocationTimer.Update(gameTime)) {
						angle = util.getAngle(hero.Position, Position);
						currentJumpPower = 8f;
						locationChoosed = true;
					}
				}
			} if (currentAI.CurrentState == State.Charge) {
				if (currentAI.LastState == State.PrepareToCharge) {
					directionX = DirectionToPlayerX(hero);
					directionY = DirectionToPlayerY(hero);
					locationChoosed = false;
				}
				if (currentJumpPower > 0) currentJumpPower -= 0.2f;
				Push((float)Math.Cos(angle) * currentJumpPower, (float)Math.Sin(angle) * currentJumpPower);
			}

		}
		#endregion

		public override void UpdateEnemie(GameTime gameTime, Player hero)
		{
			base.UpdateEnemie(gameTime, hero);
			CheckAI(gameTime, hero);
		}

		//UPDATE & DRAW
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			UpdateAnimation(gameTime);
		}
	}
}
