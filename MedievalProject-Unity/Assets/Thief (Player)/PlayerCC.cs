using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCC : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;

    private bool onGround = false;
    private Animator animate;
    private CharacterController characterController;
    private Vector3 velocity; // Used to apply gravity
    private Vector2 mouseInput; // reeeeee
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // CHARACTER MOVEMENT:
        // Get input from the keyboard.
        float goTurn = Input.GetAxis("Horizontal");
        float goMove = Input.GetAxis("Vertical");

        // Get keyboard input for strafing
        moveInput = new Vector2(goTurn, goMove);

        // Get mouse input for turning
        mouseInput = Mouse.current.delta.ReadValue();

        // Rotate the player based on mouse input
        Vector3 playerRotation = Vector3.up * mouseInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(playerRotation);

        // Move player sideways.
        Vector3 movement = transform.right * strafeSpeed * moveInput.x + transform.forward * moveSpeed * moveInput.y;
        characterController.Move(movement * Time.deltaTime);
        
        // Allow player to jump.
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            onGround = false;
        }

        // Apply gravity & vertical movement
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Configure player's animations. ~TW
        if (goMove+goTurn != 0) // && onGround
        {
            if (Input.GetKey(KeyCode.S)) {
                animate.SetInteger("Speed", -1);
            } 
            if (Input.GetKey(KeyCode.W)) {
                animate.SetInteger("Speed", 6);
            } 
            if (Input.GetKey(KeyCode.A)) {
                animate.SetInteger("Speed", 5);
            } 
            if (Input.GetKey(KeyCode.D)) {
                animate.SetInteger("Speed", 5);
            }
        } else {
            animate.SetInteger("Speed", 0);
        } 

        animate.SetBool("inAir", !onGround);
    }

    // Detect if player is touching ground
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            velocity.y = 0f; // Reset vertical velocity when on the ground
        }
        else
        {
            onGround = false;
        }
    }
}
