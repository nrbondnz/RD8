using System;
using UnityEngine;

namespace Key
{
    /// <summary>
    /// Selects an object in the keyActionController and performs an action pregessively over a time period
    /// Open: Moves the object that was an obstical out of the way over a time period
    /// </summary>
    public class OpenKeyAction : MonoBehaviour, IKeyAction
    {
        

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            KeyActionController actorObject,
            float actionSpeed)
        {
            if (!isActionStarted) return actionTime;
            actionTime -= Time.deltaTime;
            actorObject.transform.Translate(Vector3.down * Time.deltaTime * actionSpeed);
            if (actionTime < 0)
            {
                actorObject.gameObject.SetActive(false);
            }

            return actionTime;
        }
    }
}