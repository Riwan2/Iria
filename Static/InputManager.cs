using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Iria
{
	public static class InputManager
	{
		//FIELDS
		private static KeyboardState newKeyState;
		private static KeyboardState oldKeyState;
		private static MouseState newMouseState;
		private static MouseState oldMouseState;
		private static bool mouseWheelRight;
		private static bool mouseWheelLeft;

		//PROPERTIES
		public static Point MousePos { get; private set; }
		public static bool MouseWheelRight { get { return mouseWheelRight; } }
		public static bool MouseWheelLeft { get { return mouseWheelLeft; } }

		public enum eAction
		{
			Up,
			Right,
			Down,
			Left,
			MoveUp,
			MoveRight,
			MoveDown,
			MoveLeft,
			Attack,
			Confirm,
			Quit,
			MapEditor,
			HitBox,
			Restart,
			Pause,
			Tab,
			A,
			E,
		}

		private static Dictionary<eAction, Keys> Action = new Dictionary<eAction, Keys>()
		{
			{ eAction.MoveUp, Keys.Z },
			{ eAction.MoveRight, Keys.D },
			{ eAction.MoveDown, Keys.S },
			{ eAction.MoveLeft, Keys.Q },
			{ eAction.Attack, Keys.Space },
			{ eAction.Confirm, Keys.Enter },
			{ eAction.Up, Keys.Up },
			{ eAction.Right, Keys.Right },
			{ eAction.Down, Keys.Down },
			{ eAction.Left, Keys.Left },
			{ eAction.Quit, Keys.Escape },
			{ eAction.MapEditor, Keys.F2 },
			{ eAction.HitBox, Keys.F1 },
			{ eAction.Restart, Keys.R },
			{ eAction.Pause, Keys.P },
			{ eAction.Tab, Keys.Tab },
			{ eAction.A, Keys.A },
			{ eAction.E, Keys.E },
		};

		public enum eMouse
		{
			LeftClick,
			RightClick,
			MiddleClick,
		}

		public enum eCursor
		{
			CrossHair,
		}

		//METHODS
		public static void Init()
		{
			newKeyState = Keyboard.GetState();
			newMouseState = Mouse.GetState();
		}

		public static bool isHeld(eAction pAction)
		{
			if (Action.ContainsKey(pAction)) {
				return newKeyState.IsKeyDown(Action[pAction]) && oldKeyState.IsKeyDown(Action[pAction]);
			}
			return false;
		}

		public static bool JustPressed(eAction pAction)
		{
			if (Action.ContainsKey(pAction)) {
				return newKeyState.IsKeyDown(Action[pAction]) && oldKeyState.IsKeyUp(Action[pAction]);
			}
			return false;
		}

		public static bool isClicked(eMouse pMouse)
		{
			if (pMouse == eMouse.LeftClick) {
				return newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
			} else if (pMouse == eMouse.RightClick) {
				return newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
			} else if (pMouse == eMouse.MiddleClick) {
				return newMouseState.MiddleButton == ButtonState.Pressed && newMouseState.MiddleButton == ButtonState.Released;
			}
			return false;
		}

		public static bool isClickHeld(eMouse pMouse)
		{
			if (pMouse == eMouse.LeftClick) {
				return newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed;
			} else if (pMouse == eMouse.RightClick) {
				return newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Pressed;
			} else if (pMouse == eMouse.MiddleClick) {
				return newMouseState.MiddleButton == ButtonState.Pressed && newMouseState.MiddleButton == ButtonState.Pressed;
			}
			return false;
		}

		//UPDATE
		public static void Update()
		{
			oldKeyState = newKeyState;
			newKeyState = Keyboard.GetState();

			oldMouseState = newMouseState;
			newMouseState = Mouse.GetState();
			MousePos = new Point(Mouse.GetState().Position.X / Setting.Factor,
			                     Mouse.GetState().Position.Y / Setting.Factor);
			mouseWheelRight = false;
			mouseWheelLeft = false;
			if (newMouseState.ScrollWheelValue > oldMouseState.ScrollWheelValue)
				mouseWheelLeft = true;
			else if (newMouseState.ScrollWheelValue < oldMouseState.ScrollWheelValue)
				mouseWheelRight = true;
		}
	}
}
