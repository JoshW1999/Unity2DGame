using UnityEngine;

public class RegisterEnemyDamage : MonoBehaviour {
    
    // Particle Effect that plays when player is hit.
    [SerializeField] private GameObject bloodEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.CompareTag("EnemyAttack")) {

            // Particle Effect
            Instantiate(bloodEffect, transform.position, Quaternion.identity);

            // Enemy Damage to Player
            gameObject.GetComponent<PlayerResources>().health -= 10f;
            gameObject.GetComponent<PlayerResources>().healthSlider.value -= 10f;

            // SFX
            gameObject.GetComponent<SFX>().PlayPlayerGruntSFX();
        }
    }
}