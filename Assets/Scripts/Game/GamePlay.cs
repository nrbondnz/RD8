using Managers;

namespace Game
{
    public struct GamePlay
    {
        public GamePlay(bool started, int lives, GameLevel gameLevel, float timeRemaining)
        {
            Started = started;
            Lives = lives;
            GameLevel = gameLevel;
            TimeRemaining = timeRemaining;
        }

        public bool Started
        {
            get;
            set;
        }

        public int Lives { get; set; }
        public GameLevel GameLevel { get; set; }
        public float TimeRemaining { get; set; }

       

        public int RemoveLife()
        {
            return Lives--;
        }
    }
}