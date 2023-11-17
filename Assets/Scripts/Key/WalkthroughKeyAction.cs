using System;
using UnityEngine;

namespace Key
{
    public class WalkthroughKeyAction : IKeyAction
    {
        //private KeyActionController _keyActionController;
        private Renderer _renderer;
        private BoxCollider _boxCollider;

        public WalkthroughKeyAction(KeyActionController pKeyActionController)
        {
          //  _keyActionController = pKeyActionController;
            _renderer = pKeyActionController.gameObject.GetComponent<Renderer>();
            _boxCollider = pKeyActionController.gameObject.GetComponent<BoxCollider>();
        }

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed)
        {
            if (!isActionStarted) return actionTime;
            actionTime = -1f;
            //actorObject.transform.Translate(Vector3.down * Time.deltaTime * actionSpeed);
            //actorObject.gameObject.GetComponent<Material>().color = new Color(228, 243, 236, 20);
            if (actionTime < 0)
            {
                //actorObject.gameObject.se = Color.green;
                //var render = _keyActionController.gameObject.GetComponent<Renderer>();
                Material[] materials = _renderer.materials;
                //Material material = materials[0];
                if (materials.Length > 1)
                {
                    materials[0] = materials[1];
                    _renderer.materials = materials;
                }

                _boxCollider.enabled = false;
            }

            return actionTime;
        }
    }
}