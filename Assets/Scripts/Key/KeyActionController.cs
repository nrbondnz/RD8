using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Key
{/// <summary>
 /// Triggered into action with a call to CarryOutAction
 /// </summary>
    public class KeyActionController : MonoBehaviour
    {
        [SerializeField] private float actionSpeed = 2;
        [SerializeField] private float actionTime = 3;
        private bool isActionStarted;
        
        

        [FormerlySerializedAs("keyActionEnumEnum")] [SerializeField] private IKeyAction.KeyActionEnum kayActionEnum;

        private KeyAction _keyAction;

        public IKeyAction.KeyActionEnum GetKeyActionEnum()
        {
            return kayActionEnum;
        }

        /// <summary>
        /// This is called to do the (currently) one way action when the object its monitoring
        /// is triggered, likely by a collider entry
        /// Then during the fixed update the action will be carried out
        /// </summary>
        public void CarryOutAction()
        {
            this.isActionStarted = true;

        }

        /// <summary>
        /// Start is called before the first frame update
        /// Thr start call adds this KeyActionController to the KeyActionScriptSetting with the defined name
        /// </summary>
        void Start()
        {
            _keyAction = new KeyAction(this);
        }

       
        /// <summary>
        /// The IKeyAction is called on KeyAction which can be seen as the parent of the actual
        /// required action
        /// </summary>
        void FixedUpdate()
        {
            actionTime = _keyAction.DoKeyAction(
                isActionStarted,
                actionTime,
                actionSpeed);
        }
    }
}
