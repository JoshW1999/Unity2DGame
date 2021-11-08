using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerResources : MonoBehaviour
{
    public float health = 100f;
    public float stamina = 100f;
    public bool isDead = false; 

    [SerializeField] public Slider healthSlider;
    [SerializeField] public Slider staminaSlider;
    
    private Animator controller;
    private Rigidbody2D rb;
    private void Awake()
    {
        controller = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        // Note: Some Stamina drain values reside in the "PlayerMovement.cs" script, 
        // as they required more finely tuned conditions to function correctly.

        // Jumping Detector
        if (Input.GetButtonDown("Jump") && GetComponent<PlayerMovement>().isGrounded && staminaSlider.value >= 10f){
            staminaSlider.value -= 10f;
        }

        // Throw Detector - Costs 5 Stamina
        if (Input.GetButtonDown("Throw") && staminaSlider.value >= 5f) {
            staminaSlider.value -= 5f;
        }

        // Player Death Event
        if(health <= 0f && !isDead) {
            die();
        }

        // Stamina Regenerates at a fixed rate
        staminaSlider.value += 0.01f;

        // 5x Stamina Regeneration when concealed
        if (gameObject.GetComponent<PlayerMovement>().isConcealed) {
            staminaSlider.value += 0.04f;
        }
    }

    // Script to handle death of player - plays animation and begins 
    // coroutine to which waits from this animation to end before 
    // swapping scenes.
    private void die() {
        controller.SetTrigger("IsDead");
        isDead = true;
        StartCoroutine(DisplayGameOverScreen());
    }

    // Coroutine to display player death animation before swapping to respective 'Game Over' screen. 
    IEnumerator DisplayGameOverScreen() {

        yield return new WaitForSeconds(1.2f);

        if(SceneManager.GetActiveScene().name == "Game") {
            SceneManager.LoadScene("GameOverScreen 1");
        }
        else if(SceneManager.GetActiveScene().name == "Game 1") {
            SceneManager.LoadScene("GameOverScreen 2");
        }
        else {
            SceneManager.LoadScene("GameOverScreen 3");

        }

    }

}
