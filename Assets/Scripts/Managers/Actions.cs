using System;

namespace DefaultNamespace
{
    public static class Actions
    {
        public static Action<Player> onPlayerChanged;
        public static Action<GameState> OnGameStateChanged;
    }
}