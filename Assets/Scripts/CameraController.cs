using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Simple script to center camera on the player.
    private void Update() {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
