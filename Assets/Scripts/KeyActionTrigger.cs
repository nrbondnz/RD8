using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class KeyActionTrigger : MonoBehaviour
{
    [FormerlySerializedAs("unlockingSpeed")] [SerializeField] private float actionSpeed = 2;
    [FormerlySerializedAs("unlockingTime")] [SerializeField] private float actionTime = 3;
    [FormerlySerializedAs("isDoorUnlocked")] [SerializeField] private bool isDoorActionStarted = false;
    [SerializeField] private bool isReveal = false;

    public void carryOutAction()
    {
        this.isDoorActionStarted = true;
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorActionStarted  && (! isReveal ))
        {
            actionTime -= Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * this.actionSpeed);
            if (this.actionTime < 0)
            {
                gameObject.SetActive(false);
            }
        } else if (isReveal && isDoorActionStarted)
        {
            if (actionTime > 0)
            {
                actionTime -= Time.deltaTime;
                transform.Translate(Vector3.up * Time.deltaTime * this.actionSpeed);
            }

        }
    }
}
