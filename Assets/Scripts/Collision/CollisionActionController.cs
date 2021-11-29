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
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}