using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FMOD.Studio;
using static PlayerCC;
using UnityEngine.SceneManagement;

public class PlayerCC : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Quaternion initialRotation;

    [SerializeField] private float moveSpeed;
    
    [SerializeField] float attackRate = 1f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    
    public float health = 1;

    private bool onGround = false;
    private Animator animate;
    private CharacterController characterController;
    private Vector3 velocity; // Used to apply gravity
    private Vector2 mouseInput; // reeeeee
    private Vector2 moveInput;
    private float nextAttack;
    public GameObject weapon;
    private Collider weaponCollider;
    public bool isDead;
    Slider healthBar;

    [Range(0.1f, 2.0f)] // slider range
    public float sensOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        animate = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        weaponCollider = weapon.GetComponent<Collider>();
        healthBar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        sensOffset=PlayerPrefs.GetFloat("MouseSens",0.5f)*2;
    }

    // Update is called once per frame
    void Update()
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(PlayerPrefs.GetFloat("Volume", 0.5f));
        if (health<=0){
            isDead=true;
            animate.SetBool("Death", isDead);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            // Load the title screen scene
            SceneManager.LoadScene("TitleScreen");
        }
        healthBar.value = health;
        if (!isDead)
        {
            // CHARACTER MOVEMENT:
            // Get input from the keyboard.
            float goTurn = Input.GetAxis("Horizontal");
            float goMove = Input.GetAxis("Vertical");

            // Get keyboard input for strafing
            moveInput = new Vector2(goTurn, goMove);

            // Get mouse input for turning
            mouseInput = Mouse.current.delta.ReadValue() * sensOffset;

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

            // Configure player's animations for movement. ~TW
            if (goMove + goTurn != 0) // && onGround
            {
                if (Input.GetKey(KeyCode.S))
                {
                    animate.SetInteger("Speed", -1);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    animate.SetInteger("Speed", 6);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    animate.SetInteger("Speed", 3);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    animate.SetInteger("Speed", 3);
                }
            }
            else
            {
                animate.SetInteger("Speed", 0);
            }

            animate.SetBool("inAir", !onGround);

            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                animate.SetBool("Is_Attacking", true);
                weaponCollider.enabled = true;
                Invoke("AttackReset", attackRate);
            }
        }
        }

    public void RespawnPlayer()
    {
        // Disable CharacterController temporarily
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        // Reset all animator parameters
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
        }

        // Set the position and rotation to the initial values
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Re-enable CharacterController
        if (characterController != null)
        {
            characterController.enabled = true;
        }
        if (isDead)
        {
            isDead = false;
            health = 1;
        }

        Debug.Log($"After Respawn - New Position: {transform.position}");
    }
    void AttackReset(){
        weaponCollider.enabled=false;
        animate.SetBool("Is_Attacking", false);
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
