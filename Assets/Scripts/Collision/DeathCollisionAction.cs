using Game;
using UnityEngine;

namespace Collision
{
    /// <summary>
    /// 
    /// </summary>
    public class DeathCollisionAction : MonoBehaviour, ICollisionAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            GameResetManager.GetInstance().ResetAction();
            //Debug.Log("Player : " + GameObject.FindGameObjectWithTag("Player").transform.position);
            //Debug.Log("GameReset : " + transform.position);
            
            
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