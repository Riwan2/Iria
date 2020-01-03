using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public enum State
	{
		Attack,
		Walking,
		PrepareToCharge,
		Charge,
		None,
	}

	public interface AI
	{
		Direction CurrentDirection { get; }
		State CurrentState { get; }
		State LastState { get; }

		bool GoUpdate { get; }

		void Update(GameTime gameTime, Player hero, Enemie enemie);
	}
}
