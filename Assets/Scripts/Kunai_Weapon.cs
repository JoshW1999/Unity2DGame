using UnityEngine;

public class Kunai_Weapon : MonoBehaviour {

    // For Projectile Prefab and Spawn Location.
    public GameObject projectile;
    public Transform shootPosition;


    public Animator anim;
    
    private float nextThrowReady; // Can Throw next Kunai when this = 0
    [SerializeField] private float timeBetweenShots; // The period that you must wait to fire a successive shot.
    [SerializeField] private float animationLatency; // We want to wait to throw the projectile until the throw animation is complete. 

    public void Awake() {
        anim = gameObject.GetComponent<Animator>();
    }

   public void ThrowProjectile() {
        int dirFlag = 0; // 0 = facing right, 1 = facing left.

        if (!gameObject.GetComponent<PlayerMovement>().facingRight) {
            dirFlag = 1;
        }
        
        if (dirFlag == 0) {
            Instantiate(projectile, shootPosition.position, transform.rotation);
            nextThrowReady = timeBetweenShots;
        }

        else {
            // Rotating Kunai Sprite to match direction that player is facing
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            Quaternion rotKunai = Quaternion.Euler(rot);

            Instantiate(projectile, shootPosition.position, rotKunai);
            nextThrowReady = timeBetweenShots;
        }
        
    }

}
