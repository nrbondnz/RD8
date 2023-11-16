using JetBrains.Annotations;
using UnityEngine;

namespace Key
{/// <summary>
 /// Triggered into action with a call to CarryOutAction
 /// </summary>
    public class KeyActionController : MonoBehaviour
    {
        [SerializeField] private float actionSpeed = 2;
        [SerializeField] private float actionTime = 3;
        [SerializeField] private bool isActionStarted = false;
        [SerializeField] private bool isReveal = false;
        

        [SerializeField] private IKeyAction.KeyActionEnum keyActionEnumEnum;
        //private IKeyAction _keyActionInst;


        public void CarryOutAction()
        {
            this.isActionStarted = true;

        }

        /// <summary>
        /// Start is called before the first frame update
        /// Thr start call adds this KeyActionController to the KeyActionFactory with the defined name
        /// </summary>
        
        void Start()
        {
            KeyActionFactory.SetKeyAction(this, keyActionEnumEnum);
        }

        // Update is called once per frame
        /// <summary>
        /// The IKeyAction assumes one implementor in the factory, looks wrong
        /// </summary>
        void Update()
        {
            actionTime = this.gameObject.GetComponent<IKeyAction>().DoKeyAction(
                isActionStarted,
                actionTime,
                this,
                actionSpeed);

            /*if (isActionStarted  && (! isReveal ))
        {
            actionTime -= Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * this.actionSpeed);
            if (this.actionTime < 0)
            {
                gameObject.SetActive(false);
            }*/
        if (isReveal && isActionStarted)
        {
            if (actionTime > 0)
            {
                actionTime -= Time.deltaTime;
                transform.Translate(Vector3.up * Time.deltaTime * this.actionSpeed);
            }

        }
        }
    }
}
