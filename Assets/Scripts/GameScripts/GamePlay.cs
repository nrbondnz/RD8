namespace DefaultNamespace
{
    public struct GamePlay
    {
        public GamePlay(bool started, int lives, GameLevel gameLevel, float timeRemaining)
        {
            _started = started;
            _lives = lives;
            _gameLevel = gameLevel;
            _timeRemaining = timeRemaining;
        }

        public bool _started
        {
            get;
            set;
        }

        public int _lives { get; set; }
        public GameLevel _gameLevel { get; set; }
        public float _timeRemaining { get; set; }

       

        public int removeLife()
        {
            return _lives--;
        }
    }
}