using UnityEngine;

namespace TrajectoryObject
{
    /// <summary>
    /// Sim ball is a copy of the actual ball in the game which is used in the projection system
    /// </summary>
    public class SimBall : MonoBehaviour
    {

        public void Init(Vector3 velocity)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
        }


    }
}