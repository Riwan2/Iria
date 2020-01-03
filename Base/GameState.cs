using System;
namespace Iria
{
	public class GameState
	{
		private MainGame mainGame;
		public Scene CurrentScene { get; private set; }

		public enum SceneType
		{
			Menu,
			Gameplay
		}

		public GameState(MainGame pMainGame)
		{
			mainGame = pMainGame;
		}

		public void ChangeScene(SceneType pType)
		{
			if (CurrentScene != null) {
				CurrentScene.Unload();
				CurrentScene = null;
			}

			switch (pType) {
				case SceneType.Menu:
					CurrentScene = new SceneMenu(mainGame);
					break;
				case SceneType.Gameplay:
					CurrentScene = new SceneGameplay(mainGame);
					break;
				default:
					break;
			}
			CurrentScene.Load();
		}
	}
}
