using System;
using DefaultNamespace;
using UnityEngine;


    public class CameraFollow : MonoBehaviour
    {
        private Transform cameraTarget;
        [SerializeField] private Vector3 _offset;
        private float _yOffset = 0.0f;
        [SerializeField] private float lowerBound = -5.0f;
        [SerializeField] private float uppedBound = 25.0f;
        [SerializeField] private float smoothTime = 0.3f;
        private Vector3 _cameraVelocity = Vector3.zero;
        private float _xPlusZDistance;
        private Vector3 _normalizedTargetVelocity = new Vector3(0.5f, 0.0f, 0.5f);
        private Player _player = new Player();
        private Rigidbody targetRigidBody;
        public Vector3 NormalizedTargetVelocity => _normalizedTargetVelocity;

        public void OnEnable()
        {
            /*
             * This sets up s subscription to the state of the Player entity as it changes
             */
            Actions.onPlayerChanged += UpdatePlayer;
        }

        public void OnDisable()
        {
            Actions.onPlayerChanged -= UpdatePlayer;
        }

        private void Start()
        {
            
            cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
            targetRigidBody = cameraTarget.GetComponent<Rigidbody>();
            //Actions.onPlayerChanged += UpdatePlayer;
            Vector3 tempOffset = _offset;
            tempOffset.y = 0;
            _xPlusZDistance = tempOffset.magnitude;
            //offset = transform.position - cameraTarget.position;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.RightApple))
            {
                if (_yOffset < uppedBound)
                {
                    _yOffset++;
                    Debug.Log("yOffseet: " + _yOffset);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftApple))
            {
                if (_yOffset > lowerBound)
                {
                    _yOffset--;
                    Debug.Log("yOffseet: " + _yOffset);
                }
            }
        }

        
        public void UpdatePlayer(Player pPlayer)
        {
            _player = pPlayer;
            Debug.Log("Player : " + pPlayer);
        }

        private void LateUpdate()
        {
            
            //if ( (targetRigidBody.velocity.magnitude > 15.0) && ( ! _player.GoingForwards ))
            if ( ( targetRigidBody.velocity.magnitude > 8.0 ) && (  _player.GoingForwards ))
                       {
                _normalizedTargetVelocity = targetRigidBody.velocity.normalized;
                // now update the offset
                _offset.x = -(_normalizedTargetVelocity.x * _xPlusZDistance);
                _offset.z = -(_normalizedTargetVelocity.z * _xPlusZDistance);
            }
            
            
            Vector3 offsetWithY = _offset;
            offsetWithY.y += _yOffset;
            Vector3 targetPosition = cameraTarget.position + offsetWithY;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,
                ref _cameraVelocity, smoothTime);
            transform.LookAt(cameraTarget);
        }
    }
