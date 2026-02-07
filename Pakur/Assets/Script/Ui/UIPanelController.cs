using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // pause game (optional)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
