using UnityEngine;

namespace Key
{
    public class WalkthroughKeyAction : IKeyAction
    {
        private readonly Renderer _renderer;
        private readonly BoxCollider _boxCollider;

        public WalkthroughKeyAction(KeyActionController pKeyActionController)
        {
            _renderer = pKeyActionController.gameObject.GetComponent<Renderer>();
            _boxCollider = pKeyActionController.gameObject.GetComponent<BoxCollider>();
        }

        /// <summary>
        /// The Walkthrough action is immediate, the gameobject is made transparent and given a
        /// transparent color which must be set
        /// TODO make this timeout and become solid, which would mean either no way through
        /// TODO or another trigger on the other side of the transparency would be needed
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
           
                Material[] materials = _renderer.materials;
                //Material material = materials[0];
                if (materials.Length > 1)
                {
                    _renderer.material = materials[1];
                }

                _boxCollider.enabled = false;
            //}

            return actionTime;
        }
    }
}