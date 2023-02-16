using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.SceneUtils
{
    public class SlowMoButton : MonoBehaviour
    {
        public Sprite fullSpeedTex;     // the ui texture for full speed
        public Sprite slowSpeedTex;     // the ui texture for slow motion mode
        public float fullSpeed = 1;
        public float slowSpeed = 0.3f;
        public Button button;           // reference to the ui texture that will be changed


        private bool _mSlowMo;


       	void Start()
        {
			_mSlowMo = false;
        }

		void OnDestroy()
		{
			Time.timeScale = 1;
		}

        public void ChangeSpeed()
        {
            // toggle slow motion state
            _mSlowMo = !_mSlowMo;

            // update button texture
            var image = button.targetGraphic as Image;
            if (image != null)
            {
                image.sprite = _mSlowMo ? slowSpeedTex : fullSpeedTex;
            }

            button.targetGraphic = image;

			Time.timeScale = _mSlowMo ? slowSpeed : fullSpeed;
        }
    }
}
