using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    public class CollisionActionController : MonoBehaviour
    {

        [SerializeField] private ICollisionAction.CollisionActionEnum _collisionAction;

        [SerializeField] private ICollisionAction.CollisionEffectStrengthEnum _effectStrengh;
        [SerializeField] private SoundManager soundManager;

        //private IKeyAction _keyActionInst;

        public int GetCollisionActionID()
        {
            return (int)_collisionAction;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<ICollisionAction>().DoCollisionAction(this, _effectStrengh);
                soundManager.PlayCollisionSound(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<ICollisionAction>().DoCollisionAreaExitAction(this, _effectStrengh);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //KeyActionFactory.setKeyAction(this, _keyActionEnum);
            GOCollisionActionFactory.SetGameObjectCollisionAction(this, _collisionAction);
            setColorBasedOnCollisionTypeAndStrenth();
            //soundManager = GetComponent<SoundManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void setColorBasedOnCollisionTypeAndStrenth()
        {
            //int matID = (int)_collisionAction + (int)_effectStrengh;
            switch (_collisionAction)
            {
                case ICollisionAction.CollisionActionEnum.Bounce:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color(
                        (float)0.9 - ((((float) (int) _effectStrengh) / (float) 15.0) * (float)0.5), 
                        (float)1.0, 
                        (float)0.25);
                    break;
                case ICollisionAction.CollisionActionEnum.SpeedChange:
                {
                    Color col = new Color((float) 1.0, (float) 1.0 - (((float) (int) _effectStrengh) / (float) 15.0),
                        (float) 0.255);
                    gameObject.GetComponent<MeshRenderer>().material.color = col;
                    Debug.Log("Color: r : " + col.r + " g : " + col.g + " b " + col.b);
                    break;
                }
                case ICollisionAction.CollisionActionEnum.Death:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color((float)0.1, (float)0.1, (float)0.1);
                    break;
                case ICollisionAction.CollisionActionEnum.Attract:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color((float) 0.021,
                        (float) 0.65 - ((((float) (int) _effectStrengh) / (float) 15.0) * (float) 0.55),
                        (float) 0.75);
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            setColorBasedOnCollisionTypeAndStrenth();
        }
    }
}