using UnityEngine;


public class EnemyResources : MonoBehaviour
{
    // GameObject features and Player
    private Rigidbody2D rb;
    private Animator controller;
    private GameObject player;

    // Items that Enemy can drop
    [SerializeField] GameObject healthPot;
    [SerializeField] GameObject staminaPot;
    [SerializeField] GameObject kunai;



    // Enemy Health
    [SerializeField] public float enemyHealth = 100f;
    [SerializeField] private bool isDead = false;

    // Enemy Movement
    public bool moveRight;
    [SerializeField] public bool playerDetected = false;
    [SerializeField] public bool disableWalkcycle = false;
    [SerializeField] public bool seeConcealed = false; // Whether enemy patrols or not.
    [SerializeField] public bool isStationary = false; // Whether enemy patrols or not.
    [SerializeField] public bool facingRight = true;
    [SerializeField] public float patrolDistance = 5.0f; // Distance that enemy patrols, default is 5.0f units.
    [SerializeField] public float movementSpeed = 4.0f;
    private Vector3 spawnPosition; // This is the position in world coordiantes where the enemy spawns.  

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        controller = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        spawnPosition = gameObject.transform.localPosition;
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            disableWalkcycle = true;
            seeConcealed = true;
            controller.SetBool("IsRunning", false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            controller.SetBool("IsRunning", true);
            disableWalkcycle = false;
        }

    }

    private void Update() {

        // Handle Enemy's Direction
        int horizontalFlag = 0; // 1 for right, -1 for left
        if (player.GetComponentInParent<Transform>().position.x + 1f < transform.position.x) {
            horizontalFlag = -1;
        }
        else if(player.GetComponentInParent<Transform>().position.x - 1f > transform.position.x) {
            horizontalFlag = 1;
        }
        
        // Enemy's Horizontal Movement
        float movementX = movementSpeed * horizontalFlag;
        Vector3 movement = Vector3.right * movementX * Time.deltaTime;


        // Enemies can detect players up to 10 units away, unless they are concealed.
        if (Mathf.Abs(player.GetComponentInParent<Transform>().position.x - gameObject.GetComponentInParent<Transform>().position.x) >= 10f) {
            playerDetected = false;
        }
        else if (player.GetComponent<PlayerMovement>().isConcealed) {
            playerDetected = false;
        }
        else {
            playerDetected = true;

        }

        // Correcting Enemy Direction
        if (movementX > 0f) {
            facingRight = true;
        }
        else if (movementX < 0f) {
            facingRight = false;
        }

        if (!facingRight && !isDead) {

            transform.localScale = new Vector3(-0.35f, 0.35f, 0.35f);
        }
        else if(facingRight && !isDead){

            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }


        // Enemy agros the Player if detected 
        if (playerDetected && !isDead) {

            if(movementX != 0) {
                controller.SetBool("IsRunning", true);
                transform.Translate(movement);
            }
            else {
                controller.SetBool("IsRunning", false);
            }
            
        }
        // If no Enemy to agro, Enemy just patrols. 
        else if(!isDead && !isStationary && !disableWalkcycle){ // Walk Cycle

            // Ensure that they patrol within a certain range of their spawn location. 
            if (spawnPosition.x - gameObject.transform.localPosition.x > patrolDistance) {
                moveRight = true; //flip direction
            }
            else if (spawnPosition.x - gameObject.transform.localPosition.x < -patrolDistance) {
                moveRight = false; //flip direction
            }

            // Correct Face Direction
            if (moveRight) {
                transform.Translate(0.5f * movementSpeed * Time.deltaTime, 0, 0);
                facingRight = true;

            }
            else {
                transform.Translate(0.5f * -movementSpeed * Time.deltaTime, 0, 0);
                facingRight = false; 

            }
            if (!facingRight) {

                transform.localScale = new Vector3(-0.35f, 0.35f, 0.35f);
            }
            else {

                transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            }
            controller.SetBool("IsRunning", true);

        }

        // Range in which enemy will swing at player.
        if (Mathf.Abs(player.GetComponentInParent<Transform>().position.x - gameObject.GetComponentInParent<Transform>().position.x) <= 1.35f && playerDetected || seeConcealed && disableWalkcycle) {
            controller.SetTrigger("IsAttacking");
        }
        
        // Death
        if (this.enemyHealth <= 0 && !isDead) {
            // Death Animation and Object Destruction plays
            controller.SetTrigger("IsDead");
            Destroy(gameObject, 1.2f); //Allows death animation to play first
            isDead = true;
            gameObject.layer = 10; // So we can avoid collision with dead bodies.

            // Item Drops, 1/15 chance for each item. 
            float randomNum = Random.Range(0f, 15f);
            if (randomNum <= 1f) {
                Instantiate(healthPot, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);
            }
            else if(randomNum <= 2f) {
                Instantiate(staminaPot, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);
            }
            else if(randomNum <= 3f) {
                Instantiate(kunai, gameObject.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.identity);

            }
        }
  
    }

}
