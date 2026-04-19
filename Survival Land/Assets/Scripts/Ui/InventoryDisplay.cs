using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject slotPrefab; // Drag your Slot Prefab here
    public Transform slotContainer; // Drag the SlotContainer (with the Grid Layout) here
    public int numberOfSlots = 20; // How many slots you want

    void Start()
    {
        GenerateSlots();
    }

    void GenerateSlots()
    {
        // Clear existing slots if any
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        // Duplicate the slots
        for (int i = 0; i < numberOfSlots; i++)
        {
            Instantiate(slotPrefab, slotContainer);
        }
    }
}