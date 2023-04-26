using System;
using UnityEngine;

namespace Utilities
{
    public class GameObjectUtilities : MonoBehaviour
    {
        public GameObject CreateNewInstanceOfGameObject(GameObject originGO, String newObjectName)
        {
            GameObject retObject = Instantiate(originGO, originGO.gameObject.transform.position, Quaternion.identity);
            retObject.name = newObjectName;
            RemoveComponentsWithJustScripts(retObject);
            return retObject;
        }

        public void RemoveComponentsWithJustScripts(GameObject gameObj)
        {
            String newObjectName = gameObj.name;
            Component[] components = gameObj.GetComponents<Component>();
            foreach (var comp in components)
            {
                if (!comp.GetType().Namespace.Equals("UnityEngine") )
                {
                    Debug.Log("Bad : " + comp);
                }
                else
                {
                    Debug.Log("good : " + comp);
                }
            }
        }
    }
}
