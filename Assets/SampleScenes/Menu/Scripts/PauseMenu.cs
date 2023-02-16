using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle _mMenuToggle;
	private float _mTimeScaleRef = 1f;
    private float _mVolumeRef = 1f;
    private bool _mPaused;


    void Awake()
    {
        _mMenuToggle = GetComponent <Toggle> ();
	}


    private void MenuOn ()
    {
        _mTimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        _mVolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        _mPaused = true;
    }


    public void MenuOff ()
    {
        Time.timeScale = _mTimeScaleRef;
        AudioListener.volume = _mVolumeRef;
        _mPaused = false;
    }


    public void OnMenuStatusChange ()
    {
        if (_mMenuToggle.isOn && !_mPaused)
        {
            MenuOn();
        }
        else if (!_mMenuToggle.isOn && _mPaused)
        {
            MenuOff();
        }
    }


#if !MOBILE_INPUT
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
		    _mMenuToggle.isOn = !_mMenuToggle.isOn;
            Cursor.visible = _mMenuToggle.isOn;//force the cursor visible if anythign had hidden it
		}
	}
#endif

}
