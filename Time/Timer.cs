using System;
using Microsoft.Xna.Framework;

namespace Iria
{
	public class Timer
	{
		//FIELD
		private int time;
		private double timer;

		//PROPERTIE
		public int Time
		{
			get { return time; }
			set { time = value; }
		}
		public double Timers
		{
			get { return timer; }
			set { timer = value; }
		}
		public float Progress
		{
			get { return (float)(timer / time); }
		}

		//CONSTRUCTOR
		public Timer(int pTime) //MS
		{
			time = pTime;
			timer = 0;
		}

		//METHOD
		public void Reset()
		{
			timer = 0;
		}

		//UPDATE
		public bool Update(GameTime gameTime)
		{
			timer += gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timer >= time) {
				timer = 0;
				return true;
			}
			return false;
		}
	}
}
