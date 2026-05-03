using UnityEngine;

public sealed class InventoryManager : MonoBehaviour
{
    // Drag your InventoryPanel here in the Inspector
    [SerializeField] private GameObject inventoryUI;

    private bool _isOpen = false;

    void Update()
    {
        // Change "i" to whatever key you prefer
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        _isOpen = !_isOpen;
        inventoryUI.SetActive(_isOpen);

        if (_isOpen)
        {
            // Unlock mouse and pause time
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            // Lock mouse back to game and resume time
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}