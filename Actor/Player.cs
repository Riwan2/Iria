using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Player : LivingEntity
	{
		//FIELD
		private Timer animationTimer;
		private Timer animationAttackTimer;
		private int yIndex;
		private Weapon currentWeapon;
		public bool collisionRight;
		public bool collisionLeft;
		public bool collisionDown;
		public bool collisionUp;

		//PROPERTIES

		//CONSTRUCTOR
		public Player(Sprite sprite, Sprite swordSprite, int x = 0, int y = 0) : base(sprite, 2f, 20, x, y)
		{
			currentDirection = Direction.Down;
			animationTimer = new Timer(400);
			animationAttackTimer = new Timer(80);
			sprite.SetSourceRectangle(0, 0);
			sprite.Layer = 0.8f;
			yIndex = 0;
			ChangeMeleeWeapon(new Sword(swordSprite));
		}

		//METHODS
		public void ChangeMeleeWeapon(MeleeWeapon weapon)
		{
			currentWeapon = weapon;
			animationAttackTimer = new Timer(weapon.MaxIndex / 4 * weapon.AttackSpeed);
		}

		#region Attack
		private void Attack(GameTime gameTime)
		{
			if (InputManager.JustPressed(InputManager.eAction.Attack) 
			    && !currentWeapon.IsAttacking && currentWeapon.CanAttack) {
				currentWeapon.IsAttacking = true;
				yIndex = 0;
				animationAttackTimer.Reset();
			}

			currentWeapon.Update(gameTime, this);
		}
		#endregion

		#region Movement
		public override void UpdateCollisionBox()
		{
			collisionBox = new Rectangle(hitBox.X + 2, hitBox.Y + Height / 2, Width - 4, Height / 2 - 2);
			collisionComponent.UpdateCollisionBox(collisionBox);
		}

		private void SetAnimation(GameTime gameTime)
		{
			if (currentWeapon.IsAttacking) {
				if (animationAttackTimer.Update(gameTime)) {
					if (yIndex < 3) yIndex++;
					else yIndex = 0;
				}
			} else if (inMovement) {
				if (animationTimer.Update(gameTime)) {
					if (yIndex < 2) yIndex++;
					else yIndex = 1;
				}
			} else {
				animationTimer.Timers = animationTimer.Time - 0.1;
				yIndex = 0;
			}

			switch (currentDirection) {
				case Direction.Down:
					if (currentWeapon.IsAttacking) sprite.SetSourceRectangle(4, yIndex);
					else sprite.SetSourceRectangle(0, yIndex);
					break;
				case Direction.Up:
					if (currentWeapon.IsAttacking) sprite.SetSourceRectangle(5, yIndex);
					else sprite.SetSourceRectangle(3, yIndex);
					break;
				case Direction.Right:
					if (currentWeapon.IsAttacking) sprite.SetSourceRectangle(7, yIndex);
					else sprite.SetSourceRectangle(2, yIndex);
					break;
				case Direction.Left:
					if (currentWeapon.IsAttacking) sprite.SetSourceRectangle(6, yIndex);
					else sprite.SetSourceRectangle(1, yIndex);
					break;
			}
		}
		private void Movement(GameTime gameTime)
		{
			if (!currentWeapon.IsAttacking) {
				if (InputManager.isHeld(InputManager.eAction.MoveRight)) {
					currentDirection = Direction.Right;
					if (!collisionComponent.CheckMapRightCollision(movementSpeed) && !collisionRight)
						Move(movementSpeed, 0);
				} else if (InputManager.isHeld(InputManager.eAction.MoveLeft)) {
					currentDirection = Direction.Left;
					if (!collisionComponent.CheckMapLeftCollision(movementSpeed) && !collisionLeft)
						Move(-movementSpeed, 0);
				} else if (InputManager.isHeld(InputManager.eAction.MoveDown)) {
					currentDirection = Direction.Down;
					if (!collisionComponent.CheckMapDownCollision(movementSpeed) && !collisionDown) 
						Move(0, movementSpeed);
				} else if (InputManager.isHeld(InputManager.eAction.MoveUp)) {
					currentDirection = Direction.Up;
					if (!collisionComponent.CheckMapUpCollision(movementSpeed) && !collisionUp) 
					    Move(0, -movementSpeed);
				}
			}

			SetAnimation(gameTime);
		}
		#endregion

		//UPDATE DRAW
		public override void Update(GameTime gameTime)
		{
			if (!Invisible) {
				Movement(gameTime);
				Attack(gameTime);
				base.Update(gameTime);

				collisionRight = false;
				collisionLeft = false;
				collisionDown = false;
				collisionUp = false;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Invisible) {
				base.Draw(spriteBatch);
				currentWeapon.Draw(spriteBatch);
			}
		}
	}
}
