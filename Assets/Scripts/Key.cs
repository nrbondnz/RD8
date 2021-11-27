using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{ 
    [SerializeField] KeyActionTrigger keyActionTrigger;    
    [SerializeField] private float keyRotationSpeed = 3;

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyActionTrigger.carryOutAction();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * keyRotationSpeed);
    }

    private void OnDrawGizmos()
    {
        if (keyActionTrigger != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, keyActionTrigger.transform.position - transform.position);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.up *2, 0.5f);
        }
    }
}
