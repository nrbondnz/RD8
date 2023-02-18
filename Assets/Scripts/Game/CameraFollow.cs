using System;
using DefaultNamespace;
using UnityEngine;



    /*
     * Gets the main Camera to follow the _cameraTarget at a specific distance.
     * It also uses waypoints to determine how to follow the _cameraTarget in a direction that points to the next waypoint.
     */
    public class CameraFollow : MonoBehaviour
    {
        private Transform _cameraTarget;
        [SerializeField] private Vector3 offset;
        private float _yOffset = 0.0f;
        [SerializeField] private float lowerBound = -5.0f;
        [SerializeField] private float uppedBound = 25.0f;
        [SerializeField] private float smoothTime = 2.0f;
        private Vector3 _cameraVelocity = Vector3.zero;
        private float _xPlusZDistance;
        private Vector3 _normalizedTargetVelocity = new Vector3(0.5f, 0.0f, 0.5f);
        private Player _player = new Player();
        private Rigidbody _targetRigidBody;
        private GameObject _currentWaypoint;
        public Vector3 NormalizedTargetVelocity => _normalizedTargetVelocity;

        private void Start()
        {
            
            _cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
            _targetRigidBody = _cameraTarget.GetComponent<Rigidbody>();
            //Actions.onPlayerChanged += UpdatePlayer;
            Vector3 tempOffset = offset;
            tempOffset.y = 0;
            _xPlusZDistance = tempOffset.magnitude;
            //offset = transform.position - cameraTarget.position;
        }

        /**
         * Allows player to move the camera up and down as works for them
         */
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
        private void LateUpdate()
        {
            
            //if ( (targetRigidBody.velocity.magnitude > 15.0) && ( ! _player.GoingForwards ))
            if (this._currentWaypoint != null)
            {
                // todo - direction from target to waypoint, normalized
                _normalizedTargetVelocity = (this._currentWaypoint.transform.position - _targetRigidBody.position).normalized;
                offset.x = -(_normalizedTargetVelocity.x * _xPlusZDistance);
                offset.z = -(_normalizedTargetVelocity.z * _xPlusZDistance);
            } else if ( ( _targetRigidBody.velocity.magnitude > 8.0 ) && (  _player.GoingForwards )) {
                _normalizedTargetVelocity = _targetRigidBody.velocity.normalized;
                // now update the offset
                offset.x = -(_normalizedTargetVelocity.x * _xPlusZDistance);
                offset.z = -(_normalizedTargetVelocity.z * _xPlusZDistance);
            }
            
            
            Vector3 offsetWithY = offset;
            offsetWithY.y += _yOffset;
            Vector3 targetPosition = _cameraTarget.position + offsetWithY;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,
                ref _cameraVelocity, smoothTime);
            transform.LookAt(_cameraTarget);
        }

        /*
         * This sets up s subscription to the state of the Player entity as it changes
         * And a subscription to current waypoint as it changes
         */
        public void OnEnable()
        {
            
            Actions.OnPlayerChanged += UpdatePlayer;
            Actions.OnWaypointUpdate += UpdateCurrentWaypoint;
        }

        public void OnDisable()
        {
            Actions.OnPlayerChanged -= UpdatePlayer;
            Actions.OnWaypointUpdate -= UpdateCurrentWaypoint;
        }
        
        public void UpdatePlayer(Player pPlayer)
        {
            _player = pPlayer;
            Debug.Log("Player : " + pPlayer);
        }

        public void UpdateCurrentWaypoint(WaypointManager pWaypointManager)
        {
            this._currentWaypoint = pWaypointManager.CurrentWaypointGameObject();
        }
        
    }
