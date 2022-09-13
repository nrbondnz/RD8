using UnityEngine;

namespace Collision
{
    public class DeathCollisionAction : MonoBehaviour, ICollisionAction
    {
        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            gameObject.GetComponent<GameResetManager>().ResetAction();
            //Debug.Log("Player : " + GameObject.FindGameObjectWithTag("Player").transform.position);
            //Debug.Log("GameReset : " + transform.position);
            
            
        }
        
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            // No action for this Collision
        }
    }
}