using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] objects;
    public Text text;

    private int _mCurrentActiveObject;


    private void OnEnable()
    {
        text.text = objects[_mCurrentActiveObject].name;
    }


    public void NextCamera()
    {
        int nextactiveobject = _mCurrentActiveObject + 1 >= objects.Length ? 0 : _mCurrentActiveObject + 1;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == nextactiveobject);
        }

        _mCurrentActiveObject = nextactiveobject;
        text.text = objects[_mCurrentActiveObject].name;
    }
}
