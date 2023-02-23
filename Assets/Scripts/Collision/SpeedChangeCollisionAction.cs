using UnityEngine;

namespace Collision
{
    /// <summary>
    /// Details the action to be taken when a collision occurs with the target.
    /// In this class the target is sped up by the collider.
    /// The strength of the effect can be specified.
    ///
    /// Each action type can be set against the collider object by adding the
    /// CollectionActionController to the collider object and setting the action name and strength.
    /// </summary>
    public class SpeedChangeCollisionAction : MonoBehaviour, ICollisionAction
    {


        /// <summary>
        /// On entering the collision zone this method is called. in this case the target is sped up through the collider.
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
        /// Speed up effect zone is exited
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