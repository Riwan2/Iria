using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class SlimeAI : BasicAI
	{
		//FIELD
		private Timer chargeCooldownTimer;
		private Timer chargeTimer;
		private Timer preparingToChargeTimer;

		//PROPERTIES

		//CONSTRUCTOR
		public SlimeAI(int pChargeTime, int pPreparingToChargeTime, int pChargeCooldown) : base(1400, 8000, 150)
		{
			chargeCooldownTimer = new Timer(pChargeCooldown);
			chargeTimer = new Timer(pChargeTime);
			preparingToChargeTimer = new Timer(pPreparingToChargeTime);
		}

		//METHODE

		//UPDATE
		public override void Update(GameTime gameTime, Player hero, Enemie enemie)
		{
			base.Update(gameTime, hero, enemie);
			if (currentState == State.Walking) {
				if (util.getDistance(hero.Position, enemie.Position) < aggroDistance) {
					if (lastState == State.Walking) {
						enemie.DrawAlert = true;
						chargeCooldownTimer.Timers = chargeCooldownTimer.Time * 0.6f;
					}
					currentState = State.Attack;
				} else {
					if (enemie.DrawAlert) enemie.DrawAlert = false;
					chargeCooldownTimer.Reset();
					preparingToChargeTimer.Reset();
					currentState = State.Walking;
				}
			}

			if (currentState == State.Attack) {
				if (chargeCooldownTimer.Update(gameTime)) {
					currentState = State.PrepareToCharge;
				}
			} else if (currentState == State.PrepareToCharge) {
				if (preparingToChargeTimer.Update(gameTime)) {
					currentState = State.Charge;
				}
			}else if (currentState == State.Charge) {
				if (chargeTimer.Update(gameTime)) {
					currentState = State.Attack;
				}
			}
		}
	}
}
