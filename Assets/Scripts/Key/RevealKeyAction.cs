using UnityEngine;

namespace key
{
    public class RevealKeyAction : MonoBehaviour, IKeyAction
    {

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            KeyActionController actorObject,
            float actionSpeed)
        {
            if (!isActionStarted || (actionTime < 0)) return actionTime;
            actionTime -= Time.deltaTime;
            actorObject.transform.Translate(Vector3.up * Time.deltaTime * actionSpeed);
            return actionTime;
        }
    }

}