using UnityEngine;

namespace Collision
{
    /// <summary>
    /// 
    /// </summary>
    public class SpeedChangeCollisionAction : MonoBehaviour, ICollisionAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collisionEffectStrengthEnum"></param>
        public SpeedChangeCollisionAction(CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            Vector3 playerMoveVector = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity; 
                
            Debug.Log("Player : " + playerMoveVector);
            
            
            playerMoveVector.x = playerMoveVector.x * (float)1.3 * ((int)collisionEffectStrengthEnum / (int)CollisionEffectStrengthEnum.Normal);
            playerMoveVector.y = playerMoveVector.y * (float)1.3 * ((int)collisionEffectStrengthEnum / (int)CollisionEffectStrengthEnum.Normal);
            playerMoveVector.z = playerMoveVector.z * (float)1.3 * ((int)collisionEffectStrengthEnum / (int)CollisionEffectStrengthEnum.Normal);
            Debug.Log("Player (SpeedChange) : " + playerMoveVector);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = playerMoveVector;
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