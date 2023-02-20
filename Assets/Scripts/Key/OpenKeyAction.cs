using UnityEngine;

namespace Key
{
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