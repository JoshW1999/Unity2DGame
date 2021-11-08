using UnityEngine;

public class PickItemUp : MonoBehaviour
{
    [SerializeField] private Inventory inv;
    public GameObject itemButton; // There is a button for health and stamina potions, as well as Kunai. 
    private void Start() {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // When the player collides with a ground item (Kunai, Health/Stamina potions) 
    // We check to see if there's space in their inventory, if there is we add the item.
    // This script is attached to each groud object's prefab.
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {

            for (int i = 0; i < inv.slots.Length; i++) {

                if (inv.isOccupied[i] == false) {
                    inv.isOccupied[i] = true;
                    Instantiate(itemButton, inv.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
