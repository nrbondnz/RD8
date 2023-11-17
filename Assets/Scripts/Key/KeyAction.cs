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

        /// <summary>
        /// Sets up the key action according to the parent KeyActionController settings
        /// The action will be to call the appropriate child class
        /// </summary>
        /// <param name="pKeyActionController"></param>
        /// <exception cref="NotImplementedException"></exception>
        public  KeyAction(KeyActionController pKeyActionController)
        {
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

        /// <summary>
        /// Calls through from this as the parent to the implementing child action class
        /// </summary>
        /// <param name="isActionStarted"></param>
        /// <param name="actionTime"></param>
        /// <param name="actionSpeed"></param>
        /// <returns></returns>
        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed)
        {
            return _keyAction.DoKeyAction(isActionStarted, actionTime, actionSpeed);
        }
    }
}