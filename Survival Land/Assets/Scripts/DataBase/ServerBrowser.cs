using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // If using standard UI
using System.Collections;
using System.Collections.Generic;

public class ServerBrowser : MonoBehaviour
{
    public string serverListUrl = "http://127.0.0.1:8000/api/servers/";
    public GameObject serverButtonPrefab; // Create a button prefab
    public Transform contentPanel;        // The Content object of a ScrollView

    void Start()
    {
        RefreshList();
    }

    public void RefreshList()
    {
        // Clear old buttons first
        foreach (Transform child in contentPanel) Destroy(child.gameObject);
        StartCoroutine(FetchServers());
    }

    IEnumerator FetchServers()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverListUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                ServerListResponse response = JsonUtility.FromJson<ServerListResponse>(request.downloadHandler.text);
                PopulateUI(response.servers);
            }
        }
    }

    void PopulateUI(List<ServerEntry> servers)
    {
        foreach (ServerEntry server in servers)
        {
            GameObject btn = Instantiate(serverButtonPrefab, contentPanel);

            // Set the text (e.g., "Survival Server 1 - 10/30")
            btn.GetComponentInChildren<Text>().text = $"{server.name} ({server.current_players}/{server.max_players})";

            // Add click listener
            btn.GetComponent<Button>().onClick.AddListener(() => ConnectToServer(server));
        }
    }

    void ConnectToServer(ServerEntry server)
    {
        Debug.Log($"Connecting to {server.name} at {server.ip_address}:{server.port}");

        // 1. Pass IP/Port to your Network Manager (Mirror/Netcode)
        // 2. Load the ONLY Game scene you have
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameWorld");
    }
}