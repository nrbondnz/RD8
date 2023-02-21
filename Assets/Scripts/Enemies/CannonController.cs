using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// 
    /// </summary>
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private Transform cannonHead;
        [SerializeField] private Transform cannonTip;
        [SerializeField] private float shootingCoolDown = 3f;
        [SerializeField] private float laserPower = 100f;
    
        private bool _isPlayerInRange = false;

        private GameObject _player;

        private float _timeLeftToShoot = 0;

        private LineRenderer _cannonLaser;
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            _cannonLaser = GetComponent<LineRenderer>();
            _cannonLaser.sharedMaterial.color =Color.green;
            _cannonLaser.enabled = false;
            _player = GameObject.FindGameObjectWithTag("Player");
            _timeLeftToShoot = shootingCoolDown;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (_isPlayerInRange)
            {
                cannonHead.transform.LookAt(_player.transform);
            
                _cannonLaser.SetPosition(0,cannonTip.transform.position);
                _cannonLaser.SetPosition(1, _player.transform.position);

                _timeLeftToShoot -= Time.deltaTime;
                if (_timeLeftToShoot <= shootingCoolDown * 0.15)
                {
                    _cannonLaser.sharedMaterial.color = Color.white;
                } else if (_timeLeftToShoot <= shootingCoolDown * 0.5)
                {
                    _cannonLaser.sharedMaterial.color = Color.red;
                }

                if (_timeLeftToShoot < 0)
                {
                    _timeLeftToShoot = shootingCoolDown;
                    Vector3 directionToPushBack = _player.transform.position -
                                                  cannonTip.transform.position;
                    _player.GetComponent<Rigidbody>().AddForce(directionToPushBack * laserPower,
                        ForceMode.Impulse);
                    _cannonLaser.sharedMaterial.color = Color.green;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                _cannonLaser.enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                _cannonLaser.enabled = false;
                _timeLeftToShoot = shootingCoolDown;
                _cannonLaser.sharedMaterial.color = Color.green;
            }
        }
    }
}
