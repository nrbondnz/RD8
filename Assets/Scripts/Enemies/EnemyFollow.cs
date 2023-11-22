using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Can be applied to any moveable object, makes the object move towards the player/target
    /// Needs to have trigger range
    /// TODO: Could extend to always move towards the player/target
    /// TODO: Could extend to make it move towards where the player is going by looking at player velocity
    /// TODO: Could make this be a dual function with 'escape' player
    /// </summary>
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private Rigidbody _rb;
        private bool _isPlayerInRange;
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
        
        /// <summary>
        /// If player is within the detection range a force is applied in the direction of the player
        /// The speed attribute given to the enemy is used to determine how fast the enemy will move 
        /// </summary>
        private void FixedUpdate()
        {
            if (_isPlayerInRange)
            {
                Vector3 targetPosition = _player.transform.position - transform.position;
                _rb.AddForce(targetPosition * (speed * Time.fixedDeltaTime),ForceMode.VelocityChange);

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

 
