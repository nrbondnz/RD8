using System;

namespace key
{
    public static class KeyActionFactory
    {
        public static void setKeyAction(KeyActionController keyActionController,
            IKeyAction.KeyActionEnum keyActionEnum)
        {
            IKeyAction _keyActionInst;
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