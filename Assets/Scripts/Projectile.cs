using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float lifeTime;

    // Handle Kunai travel distance, speed, and lifetime.
    public void Start() {
        Invoke("DestroyProjectile", lifeTime);
    }
    private void Update() {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    public void DestroyProjectile() {
        Destroy(gameObject);
    }

}
