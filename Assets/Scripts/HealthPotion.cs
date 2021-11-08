using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour
{
    // Player GameObject
    private Transform player;
    private GameObject p;

    [SerializeField] private GameObject healthEffect, staminaEffect;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        p = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Check for (1,2,3,4) inputs, activate respective item (button) if it is occupied. 
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            
            // Check to see which item occupies Slot 1
            if (player.GetComponent<Inventory>().isOccupied[0]) { 
                player.GetComponentInChildren<Inventory>().slots[0].GetComponentInChildren<Button>().onClick.Invoke();
                player.GetComponent<Inventory>().isOccupied[0] = false;
            }
            

        }
        // Check to see which item occupies Slot 2
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (player.GetComponent<Inventory>().isOccupied[1]) {
                player.GetComponentInChildren<Inventory>().slots[1].GetComponentInChildren<Button>().onClick.Invoke();
                player.GetComponent<Inventory>().isOccupied[1] = false;
            }
        }
        // Check to see which item occupies Slot 3
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (player.GetComponent<Inventory>().isOccupied[2]) {
                player.GetComponentInChildren<Inventory>().slots[2].GetComponentInChildren<Button>().onClick.Invoke();
                player.GetComponent<Inventory>().isOccupied[2] = false;
            }
        }
        // Check to see which item occupies Slot 4
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (player.GetComponent<Inventory>().isOccupied[3]) {
                player.GetComponentInChildren<Inventory>().slots[3].GetComponentInChildren<Button>().onClick.Invoke();
                player.GetComponent<Inventory>().isOccupied[3] = false;
            }
        }

    }
    // Handle Health Potion Activation
    public void HandleHealthPotion() {
        // Restore the player's health completely
        player.GetComponent<PlayerResources>().health = 100f;
        player.GetComponent<PlayerResources>().healthSlider.value = 100f;

        // Play Health SFX
        p.GetComponent<SFX>().PlayDrinkPotionSFX();

        // Play health potion particle effect
        Instantiate(healthEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        // Remove slot's occupied status.
        string slotName = transform.parent.name; 
        switch (slotName) {
            case "Slot1":
                player.GetComponent<Inventory>().isOccupied[0] = false;
                break;

            case "Slot2":
                player.GetComponent<Inventory>().isOccupied[1] = false;
                break;

            case "Slot3":
                player.GetComponent<Inventory>().isOccupied[2] = false;
                break;

            case "Slot4":
                player.GetComponent<Inventory>().isOccupied[3] = false;
                break;
        }
       
        // Remove the gameObject.
        Destroy(gameObject);
    }

    // Handle Stamina Potion Activation
    public void HandleStaminaPotion() {
        // Restore player's stamina completely
        player.GetComponent<PlayerResources>().stamina = 100f;
        player.GetComponent<PlayerResources>().staminaSlider.value = 100f;

        // Play Stamina SFX
        p.GetComponent<SFX>().PlayDrinkPotionSFX();

        // Play stamina potion particle effect
        Instantiate(staminaEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        
        string slotName = transform.parent.name;
        switch (slotName) {
            case "Slot1":
                player.GetComponent<Inventory>().isOccupied[0] = false;
                break;

            case "Slot2":
                player.GetComponent<Inventory>().isOccupied[1] = false;
                break;

            case "Slot3":
                player.GetComponent<Inventory>().isOccupied[2] = false;
                break;

            case "Slot4":
                player.GetComponent<Inventory>().isOccupied[3] = false;
                break;
        }

        Destroy(gameObject);
    }

    public void HandleKunai() {
        // Just a different key to throw available Kunai with.
        p.GetComponent<PlayerMovement>().GetComponent<Animator>().SetTrigger("IsThrowing");

        // Still Drains Stamina
        player.GetComponent<PlayerResources>().stamina -= 5f;
        player.GetComponent<PlayerResources>().staminaSlider.value -= 5f;

        string slotName = transform.parent.name;
        switch (slotName) {
            case "Slot1":
                player.GetComponent<Inventory>().isOccupied[0] = false;
                break;

            case "Slot2":
                player.GetComponent<Inventory>().isOccupied[1] = false;
                break;

            case "Slot3":
                player.GetComponent<Inventory>().isOccupied[2] = false;
                break;

            case "Slot4":
                player.GetComponent<Inventory>().isOccupied[3] = false;
                break;
        }

        Destroy(gameObject);
    }
}
