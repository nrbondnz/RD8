using Unity.VisualScripting;
using UnityEngine;


    public class KeyActionController : MonoBehaviour
    {
        [SerializeField] private float actionSpeed = 2; 
        [SerializeField] private float actionTime = 3;
        [SerializeField] private bool isActionStarted = false;
        [SerializeField] private bool isReveal = false;
        [SerializeField] private IKeyAction.KeyAction _keyActionEnum;
        private IKeyAction _keyActionInst;
    

        public void CarryOutAction()
        {
            this.isActionStarted = true;
        
        }
    
        // Start is called before the first frame update
        void Start()
        {
            if (_keyActionEnum == IKeyAction.KeyAction.Reveal)
            {
                _keyActionInst = this.AddComponent<RevealKeyAction>();
            }
            else
            {
                _keyActionInst = this.AddComponent<OpenKeyAction>();
            }
        }

        // Update is called once per frame
        void Update()
        {
        
            actionTime = _keyActionInst.DoKeyAction(isActionStarted, 
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
            }
        } else if (isReveal && isActionStarted)
        {
            if (actionTime > 0)
            {
                actionTime -= Time.deltaTime;
                transform.Translate(Vector3.up * Time.deltaTime * this.actionSpeed);
            }

        }*/
        }
    }

