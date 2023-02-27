using Managers;

namespace Managers
{
    /// <summary>
    /// GamePlay tracks the progress of the players game, is it running, lives, gale level amd time remaining
    /// Its just a struct so any business logic must be handled by calling code
    /// </summary>
    public struct GameStatus
    {
        
        public bool Started
        {
            get;
            set;
        }

        public int Lives { get; set; }
        public GameDifficulty GameDifficulty { get; set; }
        public float TimeRemaining { get; set; }
        
        public int RemoveLife()
        {
            return Lives--;
        }
    }
}