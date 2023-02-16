using UnityEngine;

namespace Collision
{
    public class DeathCollisionAction : MonoBehaviour, ICollisionAction
    {
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            GameResetManager.GetInstance().ResetAction();
            //Debug.Log("Player : " + GameObject.FindGameObjectWithTag("Player").transform.position);
            //Debug.Log("GameReset : " + transform.position);
            
            
        }
        
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            // No action for this Collision
        }
    }
}