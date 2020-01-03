using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class MeleeWeapon : Weapon
	{
		//FIELD
		protected int attackBoxWidth;
		protected int attackBoxHeight;
		private Rectangle attackBox;
		private Timer durationTimer;
		private Timer attackSpeedTimer;
		private int index;
		private Direction currentDirection;
		private Color colorBox = new Color(230, 150, 230, 150);

		//PROPERTIE
		public int AttackSpeed { get { return attackSpeedTimer.Time; } }

		//CONSTRUTOR
		public MeleeWeapon(Sprite pSprite, int pDamage, int pAttackSpeed, int pCooldown, 
		                   int pAttackBoxWidth = 0, int pAttackBoxHeight = 0) 
			: base(pSprite, pDamage, pCooldown)
		{
			if (pAttackBoxWidth == 0) attackBoxWidth = sprite.Width;
			else attackBoxWidth = pAttackBoxWidth;
			if (pAttackBoxHeight == 0) attackBoxHeight = sprite.Height;
			else attackBoxHeight = pAttackBoxHeight;
			attackSpeedTimer = new Timer(pAttackSpeed);
			durationTimer = new Timer(pAttackSpeed * pSprite.MaxIndex);
			index = 0;
		}
		//METHOD
		#region Animation
		private void UpdateAnimation(GameTime gameTime)
		{
			if (attackSpeedTimer.Update(gameTime)) {
				if (index < sprite.MaxIndex - 1) index++;
				else index = 0;
			}
			sprite.SetSourceRectangle(index, 0);
		}
		#endregion

		#region Attack & Pos by direction
		public virtual void Attack(Player hero)
		{
			switch (hero.CurrentDirection) {
				case Direction.Down:
					AttackDown(hero);
					currentDirection = Direction.Down;
					break;
				case Direction.Up:
					AttackUp(hero);
					currentDirection = Direction.Up;
					break;
				case Direction.Left:
					AttackLeft(hero);
					currentDirection = Direction.Left;
					break;
				case Direction.Right:
					AttackRight(hero);
					currentDirection = Direction.Right;
					break;
			}

			if (durationTimer.Progress > 0.1f && durationTimer.Progress < 0.3f) {
				foreach (Enemie enemie in SceneGameplay.Enemies) {
					if (attackBox.Intersects(enemie.HitBox)) {
						if (currentDirection == Direction.Left) enemie.GetHurt(damage, Direction.Left);
						else if (currentDirection == Direction.Right) enemie.GetHurt(damage, Direction.Right);
						else if (currentDirection == Direction.Up) enemie.GetHurt(damage, Direction.Up);
						else if (currentDirection == Direction.Down) enemie.GetHurt(damage, Direction.Down);
					}
				}
			}
		}

		public virtual void AttackRight(Player hero)
		{
			attackBox = new Rectangle(hero.Position.X,
			                          hero.Position.Y - attackBoxWidth / 2,
			                          attackBoxHeight, attackBoxWidth);
			sprite.SetPosition((int)(hero.Position.X + hero.Width / 1.2f), hero.Position.Y+5);
			sprite.Rotation = (float)(Math.PI / 2);
			sprite.Layer = 0.75f;
		}
		public virtual void AttackLeft(Player hero)
		{
			attackBox = new Rectangle(hero.Position.X - attackBoxHeight,
			                          hero.Position.Y - attackBoxWidth / 2,
			                          attackBoxHeight, attackBoxWidth);
			sprite.SetPosition((int)(hero.Position.X - hero.Width / 1.2f), hero.Position.Y);
			sprite.Rotation = (float)(-Math.PI / 2);
			sprite.Layer = 0.85f;
		}
		public virtual void AttackUp(Player hero)
		{
			attackBox = new Rectangle(hero.Position.X - attackBoxWidth / 2,
			                          hero.Position.Y - attackBoxHeight,
			                          attackBoxWidth, attackBoxHeight);
			sprite.SetPosition((int)(hero.Position.X), (int)(hero.Position.Y - hero.Height / 2f));
			sprite.Rotation = 0;
			sprite.Layer = 0.85f;
		}
		public virtual void AttackDown(Player hero)
		{
			attackBox = new Rectangle(hero.Position.X - attackBoxWidth / 2,
			                          hero.Position.Y,
			                          attackBoxWidth, attackBoxHeight);
			sprite.SetPosition((int)(hero.Position.X + hero.Width / 4.5f), (int)(hero.Position.Y + hero.Height / 1.9f));
			sprite.Rotation = (float)Math.PI;
			sprite.Layer = 0.75f;
		}
		#endregion

		//UPDATE & DRAW
		public override void Update(GameTime gameTime, Player hero)
		{
			base.Update(gameTime, hero);
			if (isAttacking) {
				Attack(hero);
				UpdateAnimation(gameTime);
				if (durationTimer.Update(gameTime)) {
					isAttacking = false;
					canAttack = false;
				}
			} else {
				index = 0;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (isAttacking) {
				if (Setting.HITBOX_VISIBLE) 
					Primitive.DrawRectangle(spriteBatch, attackBox, colorBox, 0.9f);
				base.Draw(spriteBatch);
			}
		}
	}
}
