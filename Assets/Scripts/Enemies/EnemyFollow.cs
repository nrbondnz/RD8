using UnityEngine;

namespace Enemies
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private Rigidbody _rb;
        private bool _isPlayerInRange = false;
        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
            }
        }
    }
}

 
