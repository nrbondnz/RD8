using UnityEngine;

namespace Key
{
    public class RevealKeyAction : IKeyAction {
    
        private readonly KeyActionController _keyActionController;

        public RevealKeyAction(KeyActionController pKeyActionController)
        {
            _keyActionController = pKeyActionController;
            //_renderer = pKeyActionController.gameObject.GetComponent<Renderer>();
            //_boxCollider = pKeyActionController.gameObject.GetComponent<BoxCollider>();
        }

        public float DoKeyAction(bool isActionStarted,
            float actionTime,
            float actionSpeed)
        {
            if (!isActionStarted || (actionTime < 0)) return actionTime;
            actionTime -= Time.deltaTime;
            _keyActionController.transform.Translate(Vector3.up * (Time.deltaTime * actionSpeed));
            return actionTime;
        }
    }

}