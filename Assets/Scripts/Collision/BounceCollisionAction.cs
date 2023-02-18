using UnityEngine;

namespace Collision
{
    /// <summary>
    /// 
    /// </summary>
    public class BounceCollisionAction : MonoBehaviour, ICollisionAction
    {
/// <summary>
/// 
/// </summary>
/// <param name="controller"></param>
/// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
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
                          (int)CollisionEffectStrengthEnum.Normal),
                ForceMode.Impulse);

        }
/// <summary>
/// 
/// </summary>
/// <param name="controller"></param>
/// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            // No action for this Collision
        }
    }
    
    
}