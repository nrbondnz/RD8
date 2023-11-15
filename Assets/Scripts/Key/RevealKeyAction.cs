using UnityEngine;

namespace Key
{
    public class RevealKeyAction : MonoBehaviour, IKeyAction {
    
    public void Start()
    {
        KeyActionFact.addKeyActionImplementation(IKeyAction.KeyActionEnum.Reveal, this.gameObject.GetComponent<KeyAction>());
    }

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