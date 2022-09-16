using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private float hozInput, vertInput;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 13;
    private bool _isJumpButtonPressed;
    private bool _isGrounded;
    [SerializeField] private float platformPower = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GamePlayManager.GetInstance().UpdateTimeRemaining();
        // d -> 1.0f, a -> -1.0f
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

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _isJumpButtonPressed = true;
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpButtonPressed = true;
        }

    }
    
    private void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(hozInput, 0, vertInput);
        // playerMovement = playermovement* speed;
        playerMovement *= speed;
        rb.AddForce(playerMovement, ForceMode.Acceleration);


        //create a new ray, it's center is the player position, it's direction is Vector3.Down
        Ray ray = new Ray(transform.position, Vector3.down);
        //Physics.Raycast will return true if the ray hits a collider
        //send the ray and check if it did hit anything, the ray length is going to be half of our scale(player's radius),
        //plus a small value to make sure our ray is barley longer than the player's radius
        if (Physics.Raycast(ray, transform.localScale.x / 2f + 0.01f))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        if (_isJumpButtonPressed && _isGrounded)
        {
            //if true, then add a force in the up direction of our player in the form of an impulse
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //then reset the jump variable so we don't fly to the moon :).
            _isJumpButtonPressed = false;
        }
    }
}
