using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI; // Drag your Panel here
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryUI.SetActive(isOpen);

        if (isOpen)
        {
            // Unlock mouse and stop camera movement
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f; // Optional: Pauses the game
        }
        else
        {
            // Lock mouse back to game
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f; // Resumes the game
        }
    }
}