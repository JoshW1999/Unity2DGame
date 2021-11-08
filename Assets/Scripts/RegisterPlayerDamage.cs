using UnityEngine;

public class RegisterPlayerDamage : MonoBehaviour
{
    // Particle Effect that Plays when Enemy is hit by player.
    [SerializeField] private GameObject bloodEffect;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("PlayerAttack")){

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isConcealed) {
                // Attacks from concealment always kill enemies in one hit. 
                gameObject.GetComponent<EnemyResources>().enemyHealth -= 100f;
                
                // VFX
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                
                // SFX
                gameObject.GetComponent<SFX>().PlayEnemyGruntSFX();


            }
            else { 
                // Regular Attack
                gameObject.GetComponent<EnemyResources>().enemyHealth -= 25f;
                
                // VFX
                Instantiate(bloodEffect, transform.position, Quaternion.identity);

                // SFX
                gameObject.GetComponent<SFX>().PlayEnemyGruntSFX();

            }

        }
        else if (collision.gameObject.CompareTag("PlayerJumpAttack")) {

            // Jump Attack does more damage than regular attacks.
            gameObject.GetComponent<EnemyResources>().enemyHealth -= 100f;

            // VFX
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            
            // SFX
            gameObject.GetComponent<SFX>().PlayEnemyGruntSFX();

        }

        else if (collision.gameObject.CompareTag("Kunai")) {
            // Kunai Throw 
            gameObject.GetComponent<EnemyResources>().enemyHealth -= 100f;
            collision.gameObject.GetComponent<Projectile>().DestroyProjectile();

            // VFX
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
           
            // SFX
            gameObject.GetComponent<SFX>().PlayEnemyGruntSFX();
        }
    }
    
}
