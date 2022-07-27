namespace DefaultNamespace
{
    public struct GamePlay
    {
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