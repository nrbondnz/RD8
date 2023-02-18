using System;
using Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public static class Actions
    {
        public static Action<Player.Player> OnPlayerChanged;
        public static Action<GamePlay> OnGamePlayChanged;
        public static Action<WaypointManager> OnWaypointUpdate;
    }
}