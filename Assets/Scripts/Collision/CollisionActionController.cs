using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    public class CollisionActionController : MonoBehaviour
    {

        [SerializeField] private ICollisionAction.CollisionActionEnum _collisionAction;

        [SerializeField] private ICollisionAction.CollisionEffectStrengthEnum _effectStrengh;
        //private IKeyAction _keyActionInst;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<ICollisionAction>().DoCollisionAction(this, _effectStrengh);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //KeyActionFactory.setKeyAction(this, _keyActionEnum);
            GOCollisionActionFactory.setGameObjectCollisionAction(this, _collisionAction);
            setColorBasedOnCollisionTypeAndStrenth();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void setColorBasedOnCollisionTypeAndStrenth()
        {
            int matID = (int)_collisionAction + (int)_effectStrengh;
            if (_collisionAction == ICollisionAction.CollisionActionEnum.Bounce)
            {

                gameObject.GetComponent<MeshRenderer>().material.color = new Color((float)0.34, 1 * (((float) (int) _effectStrengh) / (float) 15.0), (float)0.319);
            }
            else
            {
                Color col = new Color((float) 1.0, (float) 1.0 - (((float) (int) _effectStrengh) / (float) 15.0),
                    (float) 0.255);
                gameObject.GetComponent<MeshRenderer>().material.color = col;
                Debug.Log("Color: r : " + col.r + " g : " + col.g + " b " + col.b);
            }
        }

        private void OnDrawGizmos()
        {
            setColorBasedOnCollisionTypeAndStrenth();
        }
    }
}