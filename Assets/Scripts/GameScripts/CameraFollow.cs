using System;
using UnityEngine;


    public class CameraFollow : MonoBehaviour
    {
        private Transform cameraTarget;
        [SerializeField] private Vector3 offset;
        private float yOffset = 0.0f;
        [SerializeField] private float lowerBound = -5.0f;
        [SerializeField] private float uppedBound = 25.0f;
        [SerializeField] private float smoothTime = 0.3f;
        private Vector3 cameraVelocity = Vector3.zero;
        
        private void Start()
        {
            cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
            //offset = transform.position - cameraTarget.position;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.RightApple))
            {
                if (yOffset < uppedBound)
                {
                    yOffset++;
                    Debug.Log("yOffseet: " + yOffset);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftApple))
            {
                if (yOffset > lowerBound)
                {
                    yOffset--;
                    Debug.Log("yOffseet: " + yOffset);
                }
            }
        }

        private void LateUpdate()
        {
            Vector3 offsetWithY = offset;
            offsetWithY.y += yOffset;
            Vector3 targetPosition = cameraTarget.position + offsetWithY;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,
                ref cameraVelocity, smoothTime);
            transform.LookAt(cameraTarget);
        }
    }
