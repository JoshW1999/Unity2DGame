using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    // Horizontal Movement 
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float slideSpeed = 7.5f;
    [SerializeField] public bool isRunning = false;
    [SerializeField] public bool facingRight = true;


    // Jumping
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float fallMultiplier = 2.5f; // Player will fall faster 
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float closeEnough = 0.1f;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public bool isJumping;
    [SerializeField] private Transform footPosition;
    [SerializeField] private LayerMask groundLayers;


    // Combat
    [SerializeField] public bool isAttacking = false;
    [SerializeField] public bool isConcealed = false;


    // GameObject features
    private Rigidbody2D rb;
    private Animator controller;
    public ArrayList items;

    // Ground Items
    [SerializeField] GameObject healthPot;
    [SerializeField] GameObject staminaPot;
    [SerializeField] GameObject kunai;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();

        // Loading inventory from previous level (unless it's the first level)
        if (SceneManager.GetActiveScene().name != "Game") {

            // Load Inventory into ArrayList
            gameObject.GetComponent<InventoryManager>().LoadInventory();
            items = gameObject.GetComponent<InventoryManager>().items;

            // Spawn those items below the player at level start.
            for (int i = 0; i < items.Count; i++) {

                if(items[i].ToString() == "KunaiButton(Clone)") {
                    Instantiate(kunai, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);
                }
                else if(items[i].ToString() == "StaminaButton(Clone)") {
                    Instantiate(staminaPot, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);
                }
                else {
                    Instantiate(healthPot, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);
                }
            }
            
        }


    }

    private void Update() {

        // Retreieve the player's stamina and health values 
        Slider playerStamina = GetComponent<PlayerResources>().staminaSlider;
        Slider playerHealth = GetComponent<PlayerResources>().healthSlider;

        // Handle Jumping
        // Casting ray down from player to detect ground.
        RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, closeEnough, groundLayers);

        // Determines if ray hits ground or not, sets player action parameters accordingly. 
        if (hit.collider) {
            isGrounded = true;
            isJumping = false;
        }
        else {
            isGrounded = false;
            isJumping = true;
        }
        controller.SetBool("IsGrounded", isGrounded);

        // Checks for player pressing "Jump" button (Space bar)
        if (Input.GetButtonDown("Jump") && isGrounded && playerStamina.value >= 10f) {
            rb.AddForce(Vector3.up * jumpForce);
            controller.SetTrigger("IsJumping");
        }

        // Script for making the player fall quicker on their descent to the ground.
        if (rb.velocity.y < 0) {
            //Debug.Log("Falling Faster");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            isAttacking = false; 
        }
        // Not holding down button leads to a smaller jump. 
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            //Debug.Log("Shorter Jump");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            isAttacking = false; 

        }


        // Handle Horizontal Movement
        float movementX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        Vector3 movement = Vector3.right * movementX * Time.deltaTime;

        // Play run animation only when actually running (and not airborne)
        if (Mathf.Abs(movementX) != 0 && !isJumping) {
            controller.SetBool("IsRunning", true);
            isRunning = true;
            isAttacking = false; // Reset Attack Cycle
        }
        else {
            controller.SetBool("IsRunning", false);
            isRunning = false; 
        }

        // Flags for player direction 
        if (movementX > 0f) {
            facingRight = true;
        }
        else if (movementX < 0f) {
            facingRight = false;
        }

        // Handling Player Face Direction
        if (!facingRight) {

            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else {

            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Handling Slide - Activated by Z key
        if (Input.GetKeyDown("z") && isGrounded && isRunning && playerStamina.value >= 20f) {
            
            controller.SetTrigger("IsSliding");
            isAttacking = false; // Reset Attack Cycle

            // Costs 20 stamina to slide. 
            playerStamina.value -= 20f;

            // The slide, a horizontal impulse is applied to the character.
            if (facingRight) {
                rb.AddForce(transform.right * slideSpeed, ForceMode2D.Impulse);
            }
            else {
                rb.AddForce(-transform.right * slideSpeed, ForceMode2D.Impulse);
            }
        }

        // Translate the character along the horizontal axis
        transform.Translate(movement);
        controller.SetFloat("Speed", Mathf.Abs(movementX));


        // Handle Attacking
        if (Input.GetButtonDown("Fire1") && !isJumping && !isAttacking && playerStamina.value >= 5f) { // Left-Click
            
            controller.SetTrigger("IsAttacking");
            isAttacking = true;

            // Costs 5 stamina to attack.
            playerStamina.value -= 5f;
        }
        else if (Input.GetButtonDown("Fire1") && isJumping && !isAttacking && playerStamina.value >= 5f) {
           
            controller.SetTrigger("IsJumpAttacking");
            isAttacking = true;

            // Costs 5 stamina to attack.
            playerStamina.value -= 5f;
        }


        // Handling Kunai Throwing
        if (Input.GetButtonDown("Throw") && !isJumping && playerStamina.value >= 5f) {
            
            isAttacking = false; // Reset Attack Cycle
            
            // Get Player's Inventory.-
            var inv = gameObject.GetComponent<Inventory>();
            var kp = GameObject.FindGameObjectWithTag("KunaiPrefab");
            
            // Search for Kunai
            for (int i = 0; i < inv.slots.Length; i++) {

                if (inv.slots[i].transform.Find("KunaiButton(Clone)") != null) {

                    // Destroy object in inventory and un-occupy inventory slot.
                    inv.isOccupied[i] = false;
                    Destroy(kp);
                    controller.SetTrigger("IsThrowing");
                    break;
                }
            }
        }

        // Handling Kunai Jump-Throwing
        if (Input.GetButtonDown("Throw") && isJumping && playerStamina.value >= 5f) {
            var inv = gameObject.GetComponent<Inventory>();
            var kp = GameObject.FindGameObjectWithTag("KunaiPrefab");

            // Search Inventory for Kunai
            for (int i = 0; i < inv.slots.Length; i++) {

                if (inv.slots[i].transform.Find("KunaiButton(Clone)") != null) {

                    isAttacking = false; // Reset Attack Cycle
                    controller.SetTrigger("IsJumpThrowing");

                    // Destroy object in inventory and un-occupy inventory slot.
                    inv.isOccupied[i] = false;
                    Destroy(kp);

                    break;
                }
            }
        }

       
      
    }

    // Used by event trigger in Player_Attack.anim to allow successive attacks following the animation's ending.
    public void ResetAttack() {
        isAttacking = false;
    }

    // Detects if player has collided with "EndLevelObjects", which load the next scene.
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("EndLevel1Object")) {
            gameObject.GetComponent<InventoryManager>().SaveInventory();
            SceneManager.LoadScene("LevelComplete");
        }
        else if (collision.gameObject.CompareTag("EndLevel2Object")) {
            gameObject.GetComponent<InventoryManager>().SaveInventory();
            SceneManager.LoadScene("LevelComplete 1");

        }
        else if (collision.gameObject.CompareTag("EndLevel3Object")) {
            gameObject.GetComponent<InventoryManager>().WipeSaveFile();
            SceneManager.LoadScene("LevelComplete 2");
        }
    }


}
