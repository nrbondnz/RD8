using System;
using UnityEngine;

namespace Key
{
    /// <summary>
    /// Selects an object in the keyActionController and performs an action pregessively over a time period
    /// Open: Moves the object that was an obstical out of the way over a time period
    /// </summary>
    public class OpenKeyAction : IKeyAction
    {
        //private Renderer _renderer;
        //private BoxCollider _boxCollider;
        private readonly KeyActionController _keyActionController;

        public OpenKeyAction(KeyActionController pKeyActionController)
        {
              _keyActionController = pKeyActionController;
        }

        /// <summary>
        /// This action moves the associated object out of the way over a number of frames when triggered
        /// Only does 'down' at the moment but could be extended
        /// </summary>
        /// <param name="isActionStarted"></param>
        /// <param name="actionTime"></param>
        /// <param name="actionSpeed"></param>
        /// <returns></returns>
        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed)
        {
            if (!isActionStarted) return actionTime;
            actionTime -= Time.deltaTime;
            _keyActionController.transform.Translate(Vector3.down * (Time.deltaTime * actionSpeed));
            if (actionTime < 0)
            {
                _keyActionController.gameObject.SetActive(false);
            }

            return actionTime;
        }
    }
}