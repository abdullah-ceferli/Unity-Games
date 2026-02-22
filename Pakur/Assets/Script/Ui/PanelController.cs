using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;

    // Open panel
    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    // Close panel
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
