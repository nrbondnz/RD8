using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Collision
{
    public class AttrctCollisionAction : MonoBehaviour, ICollisionAction
    {
        private bool _collisionActive = false;
        private ICollisionAction.CollisionEffectStrengthEnum _collisionEffectStrengthEnum;
        
        public void DoCollisionAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = true;
            _collisionEffectStrengthEnum = collisionEffectStrengthEnum;
            /*Vector3 directionToPushBack = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position 
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
                                                      (int)ICollisionAction.CollisionEffectStrengthEnum.Normal),
                    ForceMode.Impulse);*/



        }

        private void FixedUpdate()
        {
            if (_collisionActive)
            {
                Vector3 positionOfPlayer =
                    GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector3 positionOfCollider = transform.position;
                Vector3 normalisedVectorToObject = -((positionOfPlayer - positionOfCollider).normalized);
                GameObject.FindGameObjectWithTag("Player").transform.position +=
                    normalisedVectorToObject * Time.deltaTime * (float)(((float)(int) _collisionEffectStrengthEnum)/ 2.0);
            }
        }
    

        public void DoCollisionAreaExitAction(CollisionActionController controller,
            ICollisionAction.CollisionEffectStrengthEnum collisionEffectStrengthEnum)
        {
            _collisionActive = false;
            // No action for this Collision
        }
    }
}