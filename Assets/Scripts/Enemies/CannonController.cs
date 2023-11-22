using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// The cannon controller allows the cannon to follow the player when in range
    /// The cannon head needs to be setup so that can rotate from a fixed point.
    /// The cannon tip is the firing point
    /// When in range the laser will show its track to the player
    /// IF the counter to firing gets towards the laser beam goes from Green to Red then White before firing
    /// and resetting the 'cooldown' time and the laser resets to green (due to the time to fire)
    /// </summary>
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private Transform cannonHead;
        [SerializeField] private Transform cannonTip;
        [SerializeField] private float shootingCoolDown = 3f;
        [SerializeField] private float laserPower = 100f;
    
        private bool _isPlayerInRange;

        private GameObject _player;

        private float _timeLeftToShoot;

        private LineRenderer _cannonLaser;
        private Rigidbody _rigidbody;

        /// <summary>
        /// Start is called before the first frame update to setup the controller ready for use
        /// </summary>
        void Start()
        {
            _rigidbody = _player.GetComponent<Rigidbody>();
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
        /// This is all achieved by defining the object to point as having two points a fixed point and a
        /// movable point which results in a vector pointing to the player.
        ///
        /// The tip is connected to the tip of the head and the laser beams from the tip to the player
        ///
        /// The laser color is set according to how much time is left in the countdown to zero(fire time)
        ///
        /// When the timer reaches zero an impulse force is applied to the player snd the timer
        /// will be reset
        /// </summary>
        void FixedUpdate()
        {
            if (_isPlayerInRange) {
                // Update the head to look straight at the player(immediately, this is important)
                // and laser beam start to end positions
                SetTheHeadAndLaserToLookAtPlayer( cannonHead, cannonTip, _player.transform, _cannonLaser);

                // decrease time to shooting
                _timeLeftToShoot -= Time.deltaTime;
                SetLaserColor(_timeLeftToShoot, _cannonLaser.sharedMaterial);

                if (_timeLeftToShoot < 0)
                {
                    FireLaser(_rigidbody, cannonTip, laserPower);
                    // reset the laser time to shoot, acting like a cooldown
                    _timeLeftToShoot = shootingCoolDown;
                }
            }
        }

        private void FireLaser(Rigidbody pPlayerRb, Transform pCannonTipTransform, float pLaserPower)
        {
            // Laser fires so force direction is worked out subtracting the cannon tip from the player transform
            Vector3 directionToPushBack = pPlayerRb.transform.position -
                                          pCannonTipTransform.position;
            // AddForce to the player object in the calculated direction and using the configured laserPower
            // Done as a ForceMode of Impulse(like a punch of directional power)
            pPlayerRb.AddForce(directionToPushBack * pLaserPower,
                ForceMode.Impulse);
        }

        /// <summary>
        /// Sets the head to look at the player
        /// Sets the laser to start at the end of the head and the other end at the player
        /// </summary>
        /// <param name="pCannonHead"></param>
        /// <param name="pCannonTip"></param>
        /// <param name="pPlayerTransform"></param>
        /// <param name="pLaserBeam"></param>
        private void SetTheHeadAndLaserToLookAtPlayer(Transform pCannonHead,
                                                        Transform pCannonTip,
                                                        Transform pPlayerTransform,
                                                        LineRenderer pLaserBeam)
        {
            pCannonHead.LookAt(pPlayerTransform);
            pLaserBeam.SetPosition(0, pCannonTip.position);
            pLaserBeam.SetPosition(1, pPlayerTransform.position);
        }

        private void SetLaserColor(float pTimeLeftToShoot, Material pMaterial)
        {
            if (pTimeLeftToShoot <= shootingCoolDown * 0.15)
            {
                pMaterial.color = Color.white;
            }
            else if (pTimeLeftToShoot <= shootingCoolDown * 0.5)
            {
                pMaterial.color = Color.red;
            }
            else
            {
                _cannonLaser.sharedMaterial.color = Color.green;
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
