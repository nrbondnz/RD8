using UnityEngine;

namespace TrajectoryObject
{
    /// <summary>
    /// Sim ball is a copy of the actual ball in the game which is used in the SimBall system
    /// </summary>
    public class SimBall : MonoBehaviour
    {

        /**private static SimBall _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("SimBall Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("SimBall Awake");
            _instance = this as SimBall;

            DontDestroyOnLoad(gameObject);
        }
        
        public static SimBall GetInstance()
        {
            return _instance;
        }**/
        
        public void Init(Vector3 velocity)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
        }


    }
    
    
}