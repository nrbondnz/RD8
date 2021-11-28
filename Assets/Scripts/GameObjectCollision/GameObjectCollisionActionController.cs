using System;
using Unity.VisualScripting;
using UnityEngine;

namespace GameObjectCollision
{
    public class GameObjectCollisionActionController : MonoBehaviour
    {

        [SerializeField] private IGameObjectCollisionAction.GameObjectCollisionActionEnum _actionEnum;
        //private IKeyAction _keyActionInst;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                this.gameObject.GetComponent<IGameObjectCollisionAction>().DoGameObjectCollisionAction(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //KeyActionFactory.setKeyAction(this, _keyActionEnum);
            GameObjectCollisionActionFactory.setGameObjectCollisionAction(this,_actionEnum);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}