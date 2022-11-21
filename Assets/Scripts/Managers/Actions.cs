using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public static class Actions
    {
        public static Action<Player> onPlayerChanged;
        public static Action<GamePlay> OnGamePlayChanged;
        public static Action<GameObject> OnWaypointUpdate;
    }
}