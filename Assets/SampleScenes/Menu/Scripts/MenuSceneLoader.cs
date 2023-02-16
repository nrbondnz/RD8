using System;
using UnityEngine;

public class MenuSceneLoader : MonoBehaviour
{
    public GameObject menuUI;

    private GameObject _mGo;

	void Awake ()
	{
	    if (_mGo == null)
	    {
	        _mGo = Instantiate(menuUI);
	    }
	}
}
