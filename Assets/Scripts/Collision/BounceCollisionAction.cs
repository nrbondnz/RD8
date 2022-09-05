using DefaultNamespace;
using UnityEngine;

namespace Collision
{
    public class BounceCollisionAction : MonoBehaviour, ICollisionAction
    {

        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            Vector3 directionToPushBack = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position 
                                          );
            Debug.Log("Player : " + GameObject.FindGameObjectWithTag("Player").transform.position);
            Debug.Log("Object : " + transform.position);
            
            directionToPushBack.x = -directionToPushBack.x;
            directionToPushBack.y = 0;
            directionToPushBack.z = -directionToPushBack.z;
            Debug.Log("Player - object : " + directionToPushBack);
            directionToPushBack = directionToPushBack.normalized;
            Debug.Log("Normalised player - object : " + directionToPushBack);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>()
                .AddForce(directionToPushBack * 12 * ((int)collisionEffectStrengthEnum / 
                          (int)ICollisionAction.CollisionEffectStrengthEnum.Normal),
                ForceMode.Impulse);

        }

        public void DoCollisionAreaExitAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            // No action for this Collision
        }
    }
    
    
}