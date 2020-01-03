using System;
namespace Iria
{
	public static class Setting
	{
		public static int Factor = 2;
		public static int SCREEN_WIDTH = 640;
		public static int SCREEN_HEIGHT = 360;
		public static int FINAL_SCREEN_WIDTH = SCREEN_WIDTH * Factor;
		public static int FINAL_SCREEN_HEIGHT = SCREEN_HEIGHT * Factor;

		public static bool IS_FULLSCREEN = false;
		public static bool IS_MOUSE_VISIBLE = false;
		public static bool HITBOX_VISIBLE = false;
	}
}
