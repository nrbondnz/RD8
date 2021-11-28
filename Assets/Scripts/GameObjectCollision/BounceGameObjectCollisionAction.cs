using UnityEngine;

namespace GameObjectCollision
{
    public class BounceGameObjectCollisionAction : MonoBehaviour, IGameObjectCollisionAction
    {
        public void DoGameObjectCollisionAction(GameObjectCollisionActionController controller)
        {
            Vector3 directionToPushBack = GameObject.FindGameObjectWithTag("Player").transform.position.normalized;
            
            directionToPushBack.x = -directionToPushBack.x;
            directionToPushBack.y = -directionToPushBack.y;
            directionToPushBack.z = -directionToPushBack.z;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>()
                .AddForce(directionToPushBack * 20,
                ForceMode.Impulse);
        }
    }
}