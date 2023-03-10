using System;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities
{
    public static class Actions
    {
        public static Action<Player.Player> OnPlayerChanged;
        public static Action<GameStatus> OnGameStatusChanged;
        public static Action<WaypointManager> OnWaypointUpdate;
    }
}