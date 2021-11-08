using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public bool[] isOccupied; // One entry for each inventory slot, isOccupied[i] is true if there is an item in slot i.
    [SerializeField] public GameObject[] slots; // The gameobject representation of each slot.


}
