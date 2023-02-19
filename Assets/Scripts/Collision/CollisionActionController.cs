using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    public class CollisionActionController : MonoBehaviour
    {

        [SerializeField] protected CollisionActionEnum collisionAction;

        [SerializeField] protected CollisionEffectStrengthEnum effectStrengh;
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CollisionActionEnum GetCollisionActionEnum()
        {
            return collisionAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CollisionEffectStrengthEnum GetCollisionEffectStrengthEnum()
        {
            return effectStrengh;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCollisionActionID()
        {
            return (int)collisionAction;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<ICollisionAction>().DoCollisionAction(this, effectStrengh);
                SoundManager.GetInstance().PlayCollisionSound(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<ICollisionAction>().DoCollisionAreaExitAction(this, effectStrengh);
            }
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            //KeyActionFactory.setKeyAction(this, _keyActionEnum);
            GoCollisionActionFactory.SetGameObjectCollisionAction(this, collisionAction);
            setColorBasedOnCollisionTypeAndStrenth();
            //soundManager = GetComponent<SoundManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        void setColorBasedOnCollisionTypeAndStrenth()
        {
            //int matID = (int)_collisionAction + (int)_effectStrengh;
            switch (collisionAction)
            {
                case CollisionActionEnum.Bounce:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color(
                        (float)0.9 - ((((float) (int) effectStrengh) / (float) 15.0) * (float)0.5), 
                        (float)1.0, 
                        (float)0.25);
                    break;
                case CollisionActionEnum.SpeedChange:
                {
                    Color col = new Color((float) 1.0, (float) 1.0 - (((float) (int) effectStrengh) / (float) 15.0),
                        (float) 0.255);
                    gameObject.GetComponent<MeshRenderer>().material.color = col;
                    Debug.Log("Color: r : " + col.r + " g : " + col.g + " b " + col.b);
                    break;
                }
                case CollisionActionEnum.Death:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color((float)0.1, (float)0.1, (float)0.1);
                    break;
                case CollisionActionEnum.Attract:
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color((float) 0.021,
                        (float) 0.65 - ((((float) (int) effectStrengh) / (float) 15.0) * (float) 0.55),
                        (float) 0.75);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDrawGizmos()
        {
            setColorBasedOnCollisionTypeAndStrenth();
        }
    }
}