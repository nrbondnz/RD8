using System;

namespace DefaultNamespace
{
    public static class KeyActionFactory
    {
        public static void setKeyAction(KeyActionController keyActionController,
            IKeyAction.KeyAction keyAction)
        {
            if (keyAction == IKeyAction.KeyAction.Reveal)
            {
                keyActionController.gameObject.AddComponent<RevealKeyAction>();
            }
            else if ( keyAction == IKeyAction.KeyAction.Open)
            {
                keyActionController.gameObject.AddComponent<OpenKeyAction>();
            }
            else if ( keyAction == IKeyAction.KeyAction.WalkThrough)
            {
                keyActionController.gameObject.AddComponent<WalkthroughKeyAction>();
            }
            else
            {
                throw new NotImplementedException("Non implemented IKeyAction used in factory");
            }
        }
    }
}