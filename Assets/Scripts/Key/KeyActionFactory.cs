using System;

namespace Key
{
    public static class KeyActionFactory
    {
        public static void SetKeyAction(KeyActionController keyActionController,
            IKeyAction.KeyActionEnum keyActionEnum)
        {
            
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