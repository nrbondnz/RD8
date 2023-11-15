using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Key
{
    public  static class KeyActionFact
    {
        private static Dictionary<IKeyAction.KeyActionEnum, KeyAction> keyActionEnumToImpl = new Dictionary<IKeyAction.KeyActionEnum, KeyAction>();

        public static void addKeyActionImplementation(IKeyAction.KeyActionEnum keyActionEnum, KeyAction pGameObject)
        {
            keyActionEnumToImpl.Add(keyActionEnum, pGameObject);
        }

        public static KeyAction getKeyActionImpl(IKeyAction.KeyActionEnum keyActionEnum)
        {
            return keyActionEnumToImpl.GetValueOrDefault(keyActionEnum);
        }
        
    }
}