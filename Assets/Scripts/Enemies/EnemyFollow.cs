using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// 
    /// </summary>
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private Rigidbody _rb;
        private bool _isPlayerInRange = false;
        private GameObject _player;

        /// <summary>
        /// Start is called before the first frame update
        /// This script is a child of the enemy object so the rigidbody is stored for use in movement
        /// </summary>
        // 
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// If player is within the detection range a force is applied in the direction of the player
        /// The speed attribute given to the enemy is used to determine how fast the enemy will move 
        /// </summary>
        private void FixedUpdate()
        {
        
            if (_isPlayerInRange)
            {
                Vector3 targetPosition = _player.transform.position - transform.position;
                _rb.AddForce(targetPosition * speed * Time.fixedDeltaTime,ForceMode.VelocityChange);

                Vector3 newVelocity = _rb.velocity;
                newVelocity.y = 0;
                _rb.velocity = newVelocity;
            }
        
        }

        /// <summary>
        /// Player is in range so begin enemy effect
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
            }
        }
        /// <summary>
        /// End the effect on the enemy object
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
            }
        }
    }
}

 
