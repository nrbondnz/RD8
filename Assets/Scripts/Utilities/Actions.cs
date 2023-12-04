using System;
using Managers;
using Managers.WaypointManagement;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities
{
    public static class Actions
    {
        //public static Action<Player.OnScreenPlayerUpdate> OnPlayerChanged;
        public static Action<GameStatus> OnGameStatusChanged;
        public static Action<WaypointManager> OnWaypointUpdate;
        public static Action<PlayerInputState> OnPlayerInput;
    }
}