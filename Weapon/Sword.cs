using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class Sword : MeleeWeapon
	{
		//FIELD

		//PROPERTIES

		//CONSTRUCTOR
		public Sword(Sprite sprite) : base(sprite, 10, 40, 250, 0, sprite.Height - 10)
		{
		}
		//METHOD
		public override void Attack(Player hero)
		{
			base.Attack(hero);
		}

		//UPDATE && DRAW
		public override void Update(GameTime gameTime, Player hero)
		{
			base.Update(gameTime, hero);
		}
	}
}
