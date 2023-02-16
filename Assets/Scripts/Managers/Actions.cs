using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public static class Actions
    {
        public static Action<Player> OnPlayerChanged;
        public static Action<GamePlay> OnGamePlayChanged;
        public static Action<WaypointManager> OnWaypointUpdate;
    }
}