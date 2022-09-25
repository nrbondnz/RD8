using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public enum GamePhases
    {
        SayHiToMum,
        GamePlaying,
        Winner,
        Loser
    }
    
    public class GameState
    {
    
        private GamePhases _gamePhases = GamePhases.SayHiToMum;
        private int sceneNum = 0;
        private int lastLevel = 0;
      

        public GameState()
        {
            _gamePhases = GamePhases.SayHiToMum;
            sceneNum = 0;
            Init();
        }

        private void Init()
        {
            lastLevel = 4;
        }

        public int LastLevel
        {
            get => lastLevel;
        }

        public GamePhases GamePhases
        {
            get => _gamePhases;
            set => _gamePhases = value;
        }

        public int SceneNum
        {
            get => sceneNum;
            set => sceneNum = value;
        }
    }
}