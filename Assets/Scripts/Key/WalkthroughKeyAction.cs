using UnityEngine;

namespace Key
{
    public class WalkthroughKeyAction : MonoBehaviour, IKeyAction
    {

        public void Start()
        {
            KeyActionFact.addKeyActionImplementation(IKeyAction.KeyActionEnum.WalkThrough, this.gameObject.GetComponent<KeyAction>());
        }
        
        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            KeyActionController actorObject,
            float actionSpeed)
        {
            if (!isActionStarted) return actionTime;
            actionTime = -1f;
            //actorObject.transform.Translate(Vector3.down * Time.deltaTime * actionSpeed);
            //actorObject.gameObject.GetComponent<Material>().color = new Color(228, 243, 236, 20);
            if (actionTime < 0)
            {
                //actorObject.gameObject.se = Color.green;
                Material[] materials = actorObject.gameObject.GetComponent<Renderer>().materials;
                //Material material = materials[0];
                if (materials.Length > 1)
                {
                    materials[0] = materials[1];
                    actorObject.gameObject.GetComponent<Renderer>().materials = materials;
                }

                actorObject.gameObject.GetComponent<BoxCollider>().enabled = false;
            }

            return actionTime;
        }
    }
}