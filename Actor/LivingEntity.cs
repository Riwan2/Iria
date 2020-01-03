using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	//ENUM
	public enum Direction
	{
		Up,
		Right,
		Down,
		Left,
		None,
	}

	public class LivingEntity : Entity
	{
		//FIELD
		protected int maxLife;
		protected int currentLife;
		protected float movementSpeed;
		protected CollisionComponent collisionComponent;
		protected Rectangle collisionBox;
		protected Direction currentDirection;
		private bool isDead;
		private Color colorBox = new Color(255, 255, 255, 230);
		protected bool knockBack;
		private Timer knockBackTimer;
		private bool invicible;
		private float knockBackPower;
		private float currentKnockBackPower;
		private Direction knockBackDirection;

		//PROPERTIES
		public bool IsDead { get { return isDead; } }
		public int Life { get { return currentLife; } }
		public Direction CurrentDirection { get { return currentDirection; } }
		public Rectangle CollisionBox { get { return collisionBox; } }

		//CONSTRUCTOR
		public LivingEntity(Sprite sprite, float pMovementSpeed, int pMaxLife,
		                    int x = 0, int y = 0) : base(sprite, x, y)
		{
			knockBackTimer = new Timer(400);
			knockBackPower = 5f;
			maxLife = pMaxLife;
			currentLife = maxLife;
			movementSpeed = pMovementSpeed;
			collisionComponent = new CollisionComponent(collisionBox);
		}

		//METHOD
		#region Hurt
		public void GetHurt(int pDamage, Direction pKnockBackDirection)
		{
			if (!invicible) {
				GetDamage(pDamage);
				knockBack = true;
				invicible = true;
				currentKnockBackPower = knockBackPower;
				knockBackDirection = pKnockBackDirection;
				sprite.Color = Color.Red;
			}
		}

		private void GetKnockBack(GameTime gameTime)
		{
			if (knockBackTimer.Update(gameTime)) {
				knockBack = false;
				invicible = false;
				sprite.Color = Color.White;
			} else {
				if (knockBackDirection == Direction.Right) Push(currentKnockBackPower, 0);
				if (knockBackDirection == Direction.Left) Push(-currentKnockBackPower, 0);
				if (knockBackDirection == Direction.Down) Push(0, currentKnockBackPower);
				if (knockBackDirection == Direction.Up) Push(0, -currentKnockBackPower);
				if (currentKnockBackPower > 0.5f) currentKnockBackPower -= 0.3f;
			}
		}
		#endregion

		public void GetDamage(int pDamage)
		{
			currentLife -= pDamage;
			if (Life <= 0)
				isDead = true;
		}

		public void GetHeal(int pHeal)
		{
			currentLife += pHeal;
			if (currentLife > maxLife)
				currentLife = maxLife;
		}

		#region basicPhysique
		public virtual void UpdateCollisionBox()
		{
			collisionBox = new Rectangle(hitBox.X, hitBox.Y + Height / 2, Width, Height / 2);
			collisionComponent.UpdateCollisionBox(collisionBox);
		}

		protected void Push(float pForceX, float pForceY)
		{
			if (pForceX > 0) {
				if (!collisionComponent.CheckMapRightCollision(pForceX)) Move(pForceX, 0);
			} else if (pForceX < 0) {
				if (!collisionComponent.CheckMapLeftCollision(pForceX)) Move(pForceX, 0);
			}
			if (pForceY > 0) {
				if (!collisionComponent.CheckMapDownCollision(pForceY)) Move(0, pForceY);
			} else if (pForceY < 0) {
				if (!collisionComponent.CheckMapUpCollision(pForceY)) Move(0, pForceY);
			}
		}
		#endregion

		//UPDATE & DRAW
		public override void Update(GameTime gameTime)
		{
			UpdateCollisionBox();
			base.Update(gameTime);
			if (knockBack) GetKnockBack(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Setting.HITBOX_VISIBLE)
				Primitive.DrawRectangle(spriteBatch, collisionBox, colorBox);
			base.Draw(spriteBatch);
		}
	}
}
