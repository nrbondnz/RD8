using System;
using UnityEngine;

namespace Key
{
    /// <summary>
    /// This class sets up and later calls the KeyActionController's key activation action
    /// Its not a unity class but uses Unity classes to perform actions against
    /// During the init, a constructor call, the key action is put in place and then later called as needed
    /// by the KeyActionController instance(which will have picked an action at config time)
    /// </summary>
    public class KeyAction : IKeyAction
    {
        private readonly IKeyAction _keyAction;
        private KeyActionController _keyActionController;

        
        public  KeyAction(KeyActionController pKeyActionController)
        {
            //= gameObject.GetComponent<KeyActionController>();
            _keyActionController = pKeyActionController;
            var keyActionEnum = pKeyActionController.GetKeyActionEnum();
            if (keyActionEnum == IKeyAction.KeyActionEnum.Reveal)
            {
                _keyAction = new RevealKeyAction(pKeyActionController);
            }
            else if ( keyActionEnum == IKeyAction.KeyActionEnum.Open)
            {
                _keyAction = new OpenKeyAction(pKeyActionController);
            }
            else if ( keyActionEnum == IKeyAction.KeyActionEnum.WalkThrough)
            {
                _keyAction = new WalkthroughKeyAction(pKeyActionController);
            }
            else
            {
                throw new NotImplementedException("Non implemented IKeyAction used :" + keyActionEnum);
            }            
        }

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed)
        {
            return _keyAction.DoKeyAction(isActionStarted, actionTime, actionSpeed);
        }
    }
}