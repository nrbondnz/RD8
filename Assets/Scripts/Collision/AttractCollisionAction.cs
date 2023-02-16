using UnityEngine;

namespace Collision
{
    public class AttractCollisionAction : MonoBehaviour, ICollisionAction
    {
        private bool _collisionActive;
        private CollisionEffectStrengthEnum _collisionEffectStrengthEnum;
        
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = true;
            _collisionEffectStrengthEnum = collisionEffectStrengthEnum;

        }

        private void FixedUpdate()
        {
            if (_collisionActive)
            {
                var positionOfPlayer =
                    GameObject.FindGameObjectWithTag("Player").transform.position;
                var positionOfCollider = transform.position;
                Vector3 normalisedVectorToObject = -((positionOfPlayer - positionOfCollider).normalized);
                GameObject.FindGameObjectWithTag("Player").transform.position +=
                    normalisedVectorToObject * Time.deltaTime * (float)(((float)(int) _collisionEffectStrengthEnum)/ 2.0);
            }
        }
    

        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = false;
            // No action for this Collision
        }
    }
}