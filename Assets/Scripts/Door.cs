using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : MonoBehaviour, IDoor
{
    [SerializeField] private float unlockingSpeed = 2;
    [SerializeField] private float unlockingTime = 3;
    [SerializeField] private bool isDoorUnlocked = false;

    public void unlockDoor()
    {
        this.isDoorUnlocked = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorUnlocked)
        {
            unlockingTime -= Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * this.unlockingSpeed);
            if (this.unlockingTime < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
