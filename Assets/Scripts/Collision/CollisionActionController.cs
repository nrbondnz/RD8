using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    /// <summary>
    /// The collision action controller is responsible for handling the collision of a GameObject.
    /// It uses an interface implemented by a number of different action classes with dofferent effects when the
    /// target enters or leaves the collision effect zone
    /// </summary>
    public class CollisionActionController : MonoBehaviour
    {

        [SerializeField] protected CollisionActionEnum collisionAction;

        [SerializeField] protected CollisionEffectStrengthEnum effectStrengh;
        

        /// <summary>
        /// The unity game object using the collision action controller defines the action(and hence the effect)
        /// This is returned to interested parties
        /// </summary>
        /// <returns></returns>
        public CollisionActionEnum GetCollisionActionEnum()
        {
            return collisionAction;
        }

        /// <summary>
        /// The unity game object using the collision action controller defines the strength of the action effect
        /// This is returned to interested parties
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
        /// This method is attached to the game object collision detector
        /// On activation, the implementation of the collision action is called to take the appropriate action
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
        /// When the collision active zone is exited the implementing class will be called
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
        /// This class sets up the appropriate collision action gameobject by calling the factory method
        /// Then when triggered for collision the appropriate collision action is called
        /// </summary>
        void Start()
        {
            //KeyActionFactory.setKeyAction(this, _keyActionEnum);
            CollisionActionFactory.SetGameObjectCollisionAction(this, collisionAction);
            setColorBasedOnCollisionTypeAndStrenth();
            //soundManager = GetComponent<SoundManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// Action plus effect strengh are used to set the color of the collider gameobject
        /// </summary>
        void setColorBasedOnCollisionTypeAndStrenth()
        {
            //int matID = (int)_collisionAction + (int)_effectStrengh;
            switch (collisionAction)
            {
                case CollisionActionEnum.Bounce:
                    gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color(
                        (float)0.9 - ((((float) (int) effectStrengh) / (float) 15.0) * (float)0.5), 
                        (float)1.0, 
                        (float)0.25);
                    break;
                case CollisionActionEnum.SpeedChange:
                {
                    Color col = new Color((float) 1.0, (float) 1.0 - (((float) (int) effectStrengh) / (float) 15.0),
                        (float) 0.255);
                    gameObject.GetComponent<Renderer>().sharedMaterial.color = col;
                    Debug.Log("Color: r : " + col.r + " g : " + col.g + " b " + col.b);
                    break;
                }
                case CollisionActionEnum.Death:
                    gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color((float)0.1, (float)0.1, (float)0.1);
                    break;
                case CollisionActionEnum.Attract:
                    gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color((float) 0.021,
                        (float) 0.65 - ((((float) (int) effectStrengh) / (float) 15.0) * (float) 0.55),
                        (float) 0.75);
                    break;
            }
        }

        /// <summary>
        /// Draws the action plus effect color effect as the color of the object even during non running mode
        /// </summary>
        private void OnDrawGizmos()
        {
            setColorBasedOnCollisionTypeAndStrenth();
        }
    }
}