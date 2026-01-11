using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject buttonListPanel;

    public void TogglePanel()
    {
        buttonListPanel.SetActive(!buttonListPanel.activeSelf);
    }
}
