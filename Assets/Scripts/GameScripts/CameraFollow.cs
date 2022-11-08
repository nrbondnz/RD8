using System;
using UnityEngine;


    public class CameraFollow : MonoBehaviour
    {
        private Transform cameraTarget;
        [SerializeField] private Vector3 offset;
        private float yOffset = 0.0f;
        [SerializeField] private float lowerBound = 5.0f;
        [SerializeField] private float uppedBound = 15.0f;
        [SerializeField] private float smoothTime = 0.3f;
        private Vector3 cameraVelocity = Vector3.zero;
        private float xPlusZDistance;
        private Vector3 normalizedTargetVelocity = new Vector3(0.5f, 0.0f, 0.5f);

        public Vector3 NormalizedTargetVelocity => normalizedTargetVelocity;

        private void Start()
        {
            
            cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
            PlayerMovement.onGoingForwards += setGoingForwards;
            Vector3 tempOffset = offset;
            tempOffset.y = 0;
            xPlusZDistance = tempOffset.magnitude;
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

        private bool _refreshPosition = false;
        public void setGoingForwards(bool goingForwards)
        {
            _refreshPosition = goingForwards;
            Debug.Log("Going forwards : " + _refreshPosition);
        }

        private void LateUpdate()
        {
            Rigidbody targetRigidBody = cameraTarget.GetComponent<Rigidbody>();
            if ( _refreshPosition )
            {
                normalizedTargetVelocity = targetRigidBody.velocity.normalized;
                _refreshPosition = false;
            }
            // now update the offset
            offset.x = -(normalizedTargetVelocity.x * xPlusZDistance);
            offset.z = -(normalizedTargetVelocity.z * xPlusZDistance);
            
            Vector3 offsetWithY = offset;
            offsetWithY.y += yOffset;
            Vector3 targetPosition = cameraTarget.position + offsetWithY;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,
                ref cameraVelocity, smoothTime);
            transform.LookAt(cameraTarget);
        }
    }
