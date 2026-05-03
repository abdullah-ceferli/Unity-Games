using UnityEngine;

public sealed class LeafItem : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI; // Drag a "Press F" UI text here
    private bool _canPickup = false;

    void Start()
    {
        if (interactionUI != null) interactionUI.SetActive(false);
    }

    void Update()
    {
        // If player is in range and presses F
        if (_canPickup && Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        // Generate random amount between 1 and 3
        int amount = Random.Range(1, 4);

        // Add to our inventory manager
        InventoryManager1.LeafCount += amount;

        Debug.Log("Picked up " + amount + " leaves!");

        // Hide UI and destroy the leaf in the scene
        if (interactionUI != null) interactionUI.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canPickup = true;
            if (interactionUI != null) interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canPickup = false;
            if (interactionUI != null) interactionUI.SetActive(false);
        }
    }
}