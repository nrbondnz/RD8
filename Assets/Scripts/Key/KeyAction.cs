using UnityEngine;

namespace Key
{
    public abstract class KeyAction : MonoBehaviour, IKeyAction
    {
        public float DoKeyAction(bool isActionStarted, float actionTime, KeyActionController actorObject, float actionSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}