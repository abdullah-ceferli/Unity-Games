using UnityEngine;
using TMPro; // Add this if you want to show the number on UI

public sealed class InventoryManager1 : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TextMeshProUGUI leafText; // Drag a UI Text here to show count

    public static int LeafCount = 0; // Stores total leaves
    private bool _isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        // Update the text in the inventory panel
        if (_isOpen && leafText != null)
        {
            leafText.text = "Leaves: " + LeafCount;
        }
    }

    public void ToggleInventory()
    {
        _isOpen = !_isOpen;
        inventoryUI.SetActive(_isOpen);

        Cursor.lockState = _isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isOpen;
        Time.timeScale = _isOpen ? 0f : 1f;
    }
}