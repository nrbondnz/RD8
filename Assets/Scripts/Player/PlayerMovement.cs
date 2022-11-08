using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private float hozInput, vertInput;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 13;
    [SerializeField] private float platformPower = 2.5f;
    [SerializeField] private SimBall _simBall;
    [SerializeField] private Projection _projection;
    [SerializeField] private LineRenderer _lineRenderer;
    private GameObject _MainCamera;
    
    
    
    private bool _isGhost;
    private readonly Player _player = new Player();

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        rb.AddForce(velocity, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        //onGoingForwards += GoingBackwards;
        rb = GetComponent<Rigidbody>();
        _MainCamera = GameObject.FindWithTag("MainCamera");
        Debug.Log("_cameraFollow set to : " + _MainCamera);
    }
    
    // Update is called once per frame
    void Update()
    {
        GamePlayManager.GetInstance().UpdateTimeRemaining();
        // d -> 1.0f, a -> -1.0f
        // TODO does not really mean this to the player, they are looking at the ball, so its ball change not x and y
        hozInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        if ((Input.acceleration.x > 0.1) || (Input.acceleration.x < -0.1))
        {
            hozInput = Input.acceleration.x * platformPower;
        }
        if ((Input.acceleration.y > 0.1) || (Input.acceleration.y < -0.1))
        {
            vertInput = Input.acceleration.y * platformPower;
         
        }
        
        if ((!_player.GoingForwards))
        {
            _player.GoingForwards = true;
            Actions.onGoingForwards(_player);
        } else if (_player.GoingForwards )
        {
            _player.GoingForwards = false;
            Actions.onGoingForwards(_player);
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _player.IsJumpButtonPressed = true;
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.IsJumpButtonPressed = true;
        }

    }
    
    private void FixedUpdate()
    {
        Vector3 forward = _MainCamera.transform.forward;
        Vector3 right = _MainCamera.transform.right;
        forward.y = 0.0f;
        right.y = 0.0f;
        forward = forward.normalized;
        right = right.normalized;
        Vector3 forwardRelativeVerticalInput = forward * (vertInput * speed);
        Vector3 rightRelativeHorizontalInput = right * (hozInput * speed);
        // playerMovement = playermovement* speed;
        Vector3 playerMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        rb.AddForce(playerMovement, ForceMode.Acceleration);
        
        //create a new ray, it's center is the player position, it's direction is Vector3.Down
        Ray ray = new Ray(transform.position, Vector3.down);
        //Physics.Raycast will return true if the ray hits a collider
        //send the ray and check if it did hit anything, the ray length is going to be half of our scale(player's radius),
        //plus a small value to make sure our ray is barley longer than the player's radius
        
        if (Physics.Raycast(ray, transform.localScale.x / 2f + 0.01f))
        {
            _player.IsGrounded = true;
            _projection.removeTrajectoryLine();
            _lineRenderer.enabled = false;
        }
        else
        {
            _player.IsGrounded = false;
            _projection.SimulateTrajectory(_simBall, rb.position, rb.velocity);
            //Vector3 down = rb.TransformDirection(Vector3.down) * 10;
            RaycastHit hitInfo;
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, rb.position);
            if (Physics.Raycast(ray, out hitInfo, 20.0f))
            {
                _lineRenderer.startColor = Color.green;
                _lineRenderer.endColor = Color.cyan;
                _lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                _lineRenderer.startColor = Color.red;
                _lineRenderer.endColor = Color.black;
                Vector3 pos = transform.position;
                pos.y = pos.y - 15.0f;
                _lineRenderer.SetPosition(1,pos);
            }
        }

        if (_player.IsJumpButtonPressed && _player.IsGrounded)
        {
            //if true, then add a force in the up direction of our player in the form of an impulse
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //then reset the jump variable so we don't fly to the moon :).
            _player.IsJumpButtonPressed = false;
        }
    }
}
