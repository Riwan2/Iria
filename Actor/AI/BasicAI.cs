using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class BasicAI : AI
	{
		//FIELD
		private Timer moveTimer;
		private int minTime;
		private int maxTime;
		private bool goUpdate;
		private int random;
		protected Direction currentDirection;
		protected State currentState;
		protected State lastState;
		protected int aggroDistance;

		//PROPERTIES
		public Direction CurrentDirection { get { return currentDirection; } }
		public bool GoUpdate { get { return goUpdate; } }
		public State CurrentState { get { return currentState; } }
		public State LastState { get { return lastState; } }

		//CONSTRUCTOR
		public BasicAI(int pMoveMinTime, int pMoveMaxTime, int pAggroDistance)
		{
			maxTime = pMoveMaxTime;
			minTime = pMoveMinTime;
			moveTimer = new Timer(GetTime());
			currentState = State.Walking;
			lastState = currentState;
			aggroDistance = util.getInt(pAggroDistance - 20, pAggroDistance + 20);
		}

		//METHODS
		private int GetTime()
		{
			return util.getInt(minTime, maxTime);
		}

		private void RandomDirection()
		{
			random = util.getInt(0, 100);
			if (random < 21) currentDirection = Direction.Down;
			else if (random < 42) currentDirection = Direction.Right;
			else if (random < 63) currentDirection = Direction.Left;
			else if (random < 84) currentDirection = Direction.Up;
			else currentDirection = Direction.None;
		}

		//UPDATE
		public virtual void Update(GameTime gameTime, Player hero, Enemie enemie)
		{
			lastState = CurrentState;
			goUpdate = false;
			if (moveTimer.Update(gameTime)) {
				RandomDirection();
				goUpdate = true;
				moveTimer.Time = GetTime();
			}
		}
	}
}
