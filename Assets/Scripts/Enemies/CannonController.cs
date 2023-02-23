using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// The cannon controller allows the cannon to follow the player when in range
    /// The cannon head needs that can rotate from a fixed point.
    /// The cannon tip is the firing point
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
        /// Start is called before the first frame update to setup the controller ready for use
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
        /// The cannon head is held in place at one end so the LookAt means the other end will
        /// move to look at the player when active.
        /// This is all achieved by defining the pbject to point as having two points a fixed point and a
        /// movable point which results in a vector pointing to the player.
        ///
        /// The tip is connected to the tip of the head and the lazer beams frpm the tip to the player
        ///
        /// The laser color is set according to how much time is left in the countdown to zero(fire time)
        ///
        /// When the timer reaches zero an impulse force is applied to the player snd the timer
        /// will be reset
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
        
        /// <summary>
        ///the target is within the zone of triggering so switch on the in range and laser tracking
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                _cannonLaser.enabled = true;
            }
        }

        /// <summary>
        /// The target is out of range of the zone of following
        /// So reset to waiting state
        /// </summary>
        /// <param name="other"></param>
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
