using System;

namespace Key
{
    public static class KeyActionScriptSetting
    {
        /// <summary>
        /// Adds the script as a component of the input keyActionController
        /// This is done here dynamically because the selection of action is in the
        /// Unity parameter at config time
        /// </summary>
        /// <param name="keyActionController"></param>
        /// <param name="keyActionEnum"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SetKeyAction(KeyActionController keyActionController)
        {
            var keyActionEnum = keyActionController.getKeyActionEnum();
            if (keyActionEnum == IKeyAction.KeyActionEnum.Reveal)
            {
                keyActionController.gameObject.AddComponent<RevealKeyAction>();
            }
            else if ( keyActionEnum == IKeyAction.KeyActionEnum.Open)
            {
                keyActionController.gameObject.AddComponent<OpenKeyAction>();
            }
            else if ( keyActionEnum == IKeyAction.KeyActionEnum.WalkThrough)
            {
                keyActionController.gameObject.AddComponent<WalkthroughKeyAction>();
            }
            else
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }

            //return _keyActionInst;
        }
    }
}