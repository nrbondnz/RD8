using UnityEngine;

namespace Collision
{
    /// <summary>
    /// Details the action to be taken when a collision occurs with the target.
    /// In this class the target is bounced away from the collider.
    /// The strength of the bounce can be specified.
    ///
    /// Each action type can be set against the collider object by adding the
    /// CollectionActionController to the collider object and setting the action name and strength.
    /// </summary>
    public class BounceCollisionAction : MonoBehaviour, ICollisionAction
    {
/// <summary>
/// On entering the collision zone this method is called. in this case the target is bounced away from the collider.
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