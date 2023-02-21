using UnityEngine;

namespace Collision
{
    /// <summary>
    /// Details the action to be taken when a collision occurs with the target.
    /// In this class the target is attracted to the collider.
    /// The strength of the attraction can be specified.
    ///
    /// Each action type can be set against the collider object by adding the
    /// CollectionActionController to the collider object and setting the action name and strength.
    /// </summary>
    public class AttractCollisionAction : MonoBehaviour, ICollisionAction
    {
        private bool _collisionActive;
        private CollisionEffectStrengthEnum _collisionEffectStrengthEnum;
        
        /// <summary>
        /// The Unity object detects a collision within its area triggering so the collision
        /// action and effect strength
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = true;
            _collisionEffectStrengthEnum = collisionEffectStrengthEnum;

        }
/// <summary>
/// The effect is applied whilst in the collision effect zone
/// </summary>
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
    
/// <summary>
/// When the player leaves the collision area the effect of attraction is stopped
/// </summary>
/// <param name="controller"></param>
/// <param name="collisionEffectStrengthEnum"></param>
        public void DoCollisionAreaExitAction(CollisionActionController controller,
            CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = false;
            // No action for this Collision
        }
    }
}