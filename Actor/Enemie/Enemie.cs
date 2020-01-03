using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iria
{
	public class Enemie : LivingEntity
	{
		//FIELD
		private Point currentTile;
		private Point destinationTile;
		private Point destinationPos;
		private bool destinationLock;
		protected AI currentAI;
		protected int damage;
		protected bool freezeAI;
		private Timer randomAttackTimer;
		private int randomAttackX;
		private int randomAttackY;
		private Timer alertTimer;
		private Sprite alertSprite;

		protected Direction directionX;
		protected Direction directionY;

		//PROPERTIES
		public int Damage { get { return damage; } }
		public State CurrentState { get { return currentAI.CurrentState; } }
		public bool DrawAlert { get; set; }

		//CONSTRUCTOR
		public Enemie(Sprite pSprite, float pSpeed, int MaxLife, int pDamage, int pAggroDistance) 
			: base(pSprite, pSpeed, MaxLife)
		{
			currentDirection = Direction.None;
			currentAI = new BasicAI(1400, 8000, pAggroDistance);
			damage = pDamage;
			randomAttackTimer = new Timer(1000);
			alertTimer = new Timer(500);
			alertSprite = new Sprite(AssetManager.Alert, 0, 0, 1, 1, true);
			RandomOffsetAttack();
		}

		//METHODS
		#region Movement
		private void SetEndMovement()
		{
			destinationLock = false;
		}
		private void CheckEndMovement()
		{
			switch (currentDirection) {
				case Direction.Up:
					if (Position.Y <= destinationPos.Y) SetEndMovement();
					break;
				case Direction.Down:
					if (Position.Y >= destinationPos.Y) SetEndMovement();
					break;
				case Direction.Left:
					if (Position.X <= destinationPos.X) SetEndMovement();
					break;
				case Direction.Right:
					if (Position.X >= destinationPos.X) SetEndMovement();
					break;
			}
		}

		private void Movement()
		{
			if (destinationLock && !knockBack) {
				if (currentDirection == Direction.Up) {
					if (collisionComponent.CheckMapUpCollision(movementSpeed)) destinationLock = false;
					else Move(0, -movementSpeed);
				} else if (currentDirection == Direction.Right) {
					if (collisionComponent.CheckMapRightCollision(movementSpeed)) destinationLock = false;
					else Move(movementSpeed, 0);
				} else if (currentDirection == Direction.Down) {
					if (collisionComponent.CheckMapDownCollision(movementSpeed)) destinationLock = false; 
					else Move(0, movementSpeed);
				} else if (currentDirection == Direction.Left) {
					if (collisionComponent.CheckMapLeftCollision(movementSpeed)) destinationLock = false;
					else Move(-movementSpeed, 0);
				}
				CheckEndMovement();
			}
		}
		#endregion

		#region TileMovement
		private void SetMovementTile(int pX, int pY)
		{
			if (pX >= 0 && pX < TileMap.MAP_WIDTH && pY >= 0 && pY < TileMap.MAP_HEIGHT) {
				currentTile = util.getTile(Position.X, Position.Y);
				destinationTile = new Point(currentTile.X + pX, currentTile.Y + pY);
				destinationPos = util.getWorldPos(destinationTile.X, destinationTile.Y);
				if (TileMap.CurrentMap.CollisionMap[destinationTile.Y, destinationTile.X] == 0) destinationLock = true;
			}
		}
		protected void MoveRight()
		{
			if (!destinationLock) {
				currentDirection = Direction.Right;
				SetMovementTile(1, 0);
			}
		}
		protected void MoveLeft()
		{
			if (!destinationLock) {
				currentDirection = Direction.Left;
				SetMovementTile(-1, 0);
			}
		}
		protected void MoveUp()
		{
			if (!destinationLock) {
				currentDirection = Direction.Up;
				SetMovementTile(0, -1);
			}
		}
		protected void MoveDown()
		{
			if (!destinationLock) {
				currentDirection = Direction.Down;
				SetMovementTile(0, 1);
			}
		}
		#endregion

		#region AI
		private void Alert(GameTime gameTime)
		{
			if (DrawAlert) {
				alertSprite.SetPosition(Position.X, hitBox.Y - 8);
				if (alertTimer.Update(gameTime)) {
					DrawAlert = false;
				}
			}
		}
		private void RandomOffsetAttack()
		{
			randomAttackX = util.getInt(-50, 50);
			randomAttackY = util.getInt(-50, 50);
		}
		private void CheckAI(GameTime gameTime, Player hero)
		{
			currentAI.Update(gameTime, hero, this);
			if (!freezeAI) {
				if (currentAI.GoUpdate && !destinationLock && currentAI.CurrentState == State.Walking) {
					if (currentAI.CurrentDirection == Direction.Right) MoveRight();
					else if (currentAI.CurrentDirection == Direction.Left) MoveLeft();
					else if (currentAI.CurrentDirection == Direction.Down) MoveDown();
					else if (currentAI.CurrentDirection == Direction.Up) MoveUp();
				} else if (currentAI.CurrentState == State.Attack) {
					Alert(gameTime);
					if (randomAttackTimer.Update(gameTime)) {
						RandomOffsetAttack();
					}
					GoToPlayer(1f, hero);
				}
			}
		}
		#endregion

		#region Movement to player 
		protected Direction DirectionToPlayerX(Player hero, int offsetX = 0)
		{
			if (Position.X < hero.Position.X + offsetX) return Direction.Right;
			if (Position.X > hero.Position.X + offsetX) return Direction.Left;
			return Direction.None;
		}
		protected Direction DirectionToPlayerY(Player hero, int offsetY = 0)
		{
			if (Position.Y < hero.Position.Y + offsetY) return Direction.Down;
			if (Position.Y > hero.Position.Y + offsetY) return Direction.Up;
			return Direction.None;
		}

		protected void GoToPlayer(float pForce, Player hero)
		{
			if (DirectionToPlayerX(hero, randomAttackX) == Direction.Right) Push(pForce, 0);
			else if (DirectionToPlayerX(hero, randomAttackX) == Direction.Left) Push(-pForce, 0);
			if (DirectionToPlayerY(hero, randomAttackY) == Direction.Down) Push(0, pForce);
			else if (DirectionToPlayerY(hero, randomAttackY) == Direction.Up) Push(0, -pForce);
		}
		#endregion

		#region Sorting
		public virtual void UpdateEnemie(GameTime gameTime, Player hero)
		{
			CheckAI(gameTime, hero);

			foreach (Enemie enemie in SceneGameplay.Enemies) {
				if (enemie == this) continue;
				if (hitBox.Intersects(enemie.HitBox)) {
					if (collisionBox.Y < enemie.CollisionBox.Y) sprite.Layer = 0.65f;
					else sprite.Layer = 0.75f;
				}

				if (collisionBox.Intersects(enemie.CollisionBox) && !knockBack && CurrentState != State.Attack) {
					if (enemie.CollisionBox.Left < collisionBox.Right && enemie.CollisionBox.Left > Position.X) {
						Push(-movementSpeed * 2, 0);
					} else if (enemie.CollisionBox.Right > collisionBox.Left && enemie.CollisionBox.Right < Position.X) {
						Push(movementSpeed * 2, 0);
					}

					if (enemie.CollisionBox.Bottom > collisionBox.Top && enemie.CollisionBox.Bottom < collisionBox.Y + collisionBox.Height / 2) {
						Push(0, movementSpeed * 2);
					} else if (enemie.CollisionBox.Top < collisionBox.Bottom && enemie.CollisionBox.Top > collisionBox.Y + collisionBox.Height / 2) {
						Push(0, -movementSpeed * 2);
					}
				}
			}

			//if (collisionBox.Intersects(hero.CollisionBox)) {
			//	if (hero.CollisionBox.Left < collisionBox.Right && hero.CollisionBox.Left > Position.X) {
			//		if (!collisionComponent.CheckMapLeftCollision(movementSpeed * 2)) Move(-movementSpeed * 2, 0);
			//		else hero.collisionLeft = true;
			//	} else if (hero.CollisionBox.Right > collisionBox.Left && hero.CollisionBox.Right < Position.X) {
			//		if (!collisionComponent.CheckMapRightCollision(movementSpeed * 2)) Move(movementSpeed * 2, 0);
			//		else hero.collisionRight = true;
			//	}

			//	if (hero.CollisionBox.Bottom > collisionBox.Top && hero.CollisionBox.Bottom < collisionBox.Y + collisionBox.Height / 2) {
			//		if (!collisionComponent.CheckMapDownCollision(movementSpeed * 2)) Move(0, movementSpeed * 2);
			//		else hero.collisionDown = true;
			//	} else if (hero.CollisionBox.Top < collisionBox.Bottom && hero.CollisionBox.Top > collisionBox.Y + collisionBox.Height / 2) {
			//		if (!collisionComponent.CheckMapUpCollision(movementSpeed * 2)) Move(0, -movementSpeed * 2);
			//		else hero.collisionUp = true;
			//	}
			//}

			if (collisionBox.Intersects(hero.CollisionBox) && 
			    (currentAI.CurrentState == State.Attack || currentAI.CurrentState == State.Charge)) {
				if (collisionBox.Right > hero.CollisionBox.Left && collisionBox.Right < hero.Position.X)
					hero.GetHurt(damage, Direction.Right);
				else if (collisionBox.Left < hero.CollisionBox.Right && collisionBox.Left > hero.Position.X)
					hero.GetHurt(damage, Direction.Left);
				if (collisionBox.Bottom > hero.CollisionBox.Top &&
					collisionBox.Bottom < hero.CollisionBox.Y + hero.CollisionBox.Height / 2)
					hero.GetHurt(damage, Direction.Down);
				if (collisionBox.Top < collisionBox.Bottom &&
					collisionBox.Top > hero.CollisionBox.Y + hero.CollisionBox.Height / 2)
					hero.GetHurt(damage, Direction.Up);
			}

			if (hitBox.Intersects(hero.HitBox)) {
				if (collisionBox.Y > hero.CollisionBox.Y) sprite.Layer = 0.75f;
				else sprite.Layer = 0.85f;
			} else sprite.Layer = 0.7f;
		}
		#endregion

		//UPDATE & DRAW
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (currentAI.CurrentState == State.Walking) {
				Movement();
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			if (DrawAlert) {
				alertSprite.Draw(spriteBatch);
			}
		}
	}
}
