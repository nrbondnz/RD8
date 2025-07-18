using System;
using System.Linq;
using Key;
using Managers.WaypointManagement;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Used to create a copy of an object and optionally remove the active scripts and leave just the physics
    /// </summary>
    public class GameObjectUtilities : MonoBehaviour
    {
        public GameObject CreateNewInstanceOfGameObject(GameObject originGo, String newObjectName,
            bool removeActiveScripts = true)
        {
            GameObject retObject = Instantiate(originGo, originGo.gameObject.transform.position, Quaternion.identity);
            retObject.name = newObjectName;
            if (removeActiveScripts)
            {
                RemoveComponentsWithJustScripts(retObject);
            }

            //retObject.gameObject.tag = newObjectName;
            return retObject;
        }

        /// <summary>
        /// Inspects the game object components and removes those with just scripts
        /// </summary>
        /// <param name="gameObj"></param>
        public void RemoveComponentsWithJustScripts(GameObject gameObj)
        {
            String newObjectName = gameObj.name;
            Component[] components = gameObj.GetComponents<Component>();
            foreach (var comp in components)
            {
                if (!comp.GetType().Namespace.Equals("UnityEngine"))
                {
                    Destroy(comp);
                    //Debug.Log("Bad : " + comp);

                }
                else
                {
                    //Debug.Log("good : " + comp);
                }
            }
        }

        public static Component GetComponentOfType<T>(GameObject gameObject)
        {
            return gameObject.GetComponents<Component>().Where(c => c is T).FirstOrDefault();
        }

        /*public static ITimeAllowedToWaypoint TimeAllowedToWaypointComponent(WaypointSubscriber gameObject)
        {
            //return (ITimeAllowedToWaypoint)GameObjectUtilities.GetComponentOfType<ITimeAllowedToWaypoint>(gameObject);
            return gameObject;
        }*/

        public static String ConvertToMinsAndSecs(float timeIn)
        {
            int mins = (int)(timeIn / 60.0);
            int secs = (int)(timeIn - (mins * 60));
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return mins + ":" + secString;
        }

        public String ConvertToSecs(float timeIn)
        {
            int secs = (int)timeIn;
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return secString;
        }
    }
}
