using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Weapon
	{
		//FIELD
		protected Sprite sprite;
		protected int damage;
		protected bool isAttacking;
		protected Timer cooldownTimer;
		protected bool canAttack;

		//PROPERTIES
		public bool IsAttacking { 
			get { return isAttacking; }
			set { isAttacking = value; } 
		}
		public bool CanAttack { get { return canAttack; } }
		public int MaxIndex { get { return sprite.MaxIndex; } }

		//CONSTRUCTOR
		public Weapon(Sprite pSprite, int pDamage, int pCooldown)
		{
			sprite = pSprite;
			sprite.Layer = 0.75f;
			damage = pDamage;
			cooldownTimer = new Timer(pCooldown);
			canAttack = true;
		}
		//METHOD

		//DRAW & UPDATE
		public virtual void Update(GameTime gameTime, Player hero)
		{
			if (!canAttack) {
				if (cooldownTimer.Update(gameTime)) 
					canAttack = true;
			}
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			sprite.Draw(spriteBatch);
		}
	}
}
