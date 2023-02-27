using Managers;
using UnityEngine;
using Utilities;

namespace Collision
{
    /// <summary>
    /// Details the action to be taken when a collision occurs with the target.
    /// In this class the target is killed when it hits the collider
    /// The strength of the death is not relevant in this case
    ///
    /// Each action type can be set against the collider object by adding the
    /// CollectionActionController to the collider object and setting the action name
    /// </summary>
    public class DeathCollisionAction : MonoBehaviour, ICollisionAction
    {
        /// <summary>
        /// This action interacts directly with the GameResetManager tp reduce a life
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            GameResetManager.GetInstance().RemoveLifeAndResetScene();
            //Debug.Log("Player : " + GameObject.FindGameObjectWithTag("Player").transform.position);
            //Debug.Log("GameReset : " + transform.position);
            
            
        }
        
        /// <summary>
        /// This will not be relevant as the game manager will take over killing the player
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