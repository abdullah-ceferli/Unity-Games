using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class UserData
{
    public string username;
    public string password;
    public string email;
    public string device_type;
    public string os_info;
}

public class RegistrationManager : MonoBehaviour
{
    private string registerUrl = "http://127.0.0.1:8000/api/register/";

    public void RegisterUser(string user, string pass, string mail)
    {
        UserData data = new UserData
        {
            username = user,
            password = pass,
            email = mail,
            device_type = SystemInfo.deviceModel,
            os_info = SystemInfo.operatingSystem
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendRequest(json));
    }

    IEnumerator SendRequest(string json)
    {
        using (UnityWebRequest request = new UnityWebRequest(registerUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}