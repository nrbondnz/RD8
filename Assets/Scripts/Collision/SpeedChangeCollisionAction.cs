using UnityEngine;

namespace Collision
{
    public class SpeedChangeCollisionAction : MonoBehaviour, ICollisionAction
    {
        public SpeedChangeCollisionAction(ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
        }

        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            Vector3 playerMoveVector = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity; 
                
            Debug.Log("Player : " + playerMoveVector);
            
            
            playerMoveVector.x = playerMoveVector.x * ((int)collisionEffectStrengthEnum / (int)ICollisionAction.CollisionEffectStrengthEnum.Normal);
            playerMoveVector.y = playerMoveVector.y * ((int)collisionEffectStrengthEnum / (int)ICollisionAction.CollisionEffectStrengthEnum.Normal);
            playerMoveVector.z = playerMoveVector.z * ((int)collisionEffectStrengthEnum / (int)ICollisionAction.CollisionEffectStrengthEnum.Normal);
            Debug.Log("Player (SpeedChange) : " + playerMoveVector);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = playerMoveVector;
        }
    }
}