using UnityEngine;

public class PlayerConceal : MonoBehaviour
{
    public bool isConcealed; 
    Rigidbody2D player;

    public void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    // Handle concealment 
    // Enter a concealable object
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isConcealed = true; 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isConcealed = true; 
        }
    }

    // Exiting a concealable object
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isConcealed = false; 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isConcealed = false; 
        }
    }
}
