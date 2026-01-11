using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Fake25SecLoader : MonoBehaviour
{
    [Header("Your Loading Text")]
    public TextMeshProUGUI loadingText;

    [Header("Messages (changes every few seconds)")]
    public string[] messages = {
        "Loading epicness...",
        "Summoning dragons...",
        "Sharpening swords...",
        "Baking cookies...",
        "Almost ready!"
    };

    [Header("Wait time in seconds (you can change this anytime)")]
    public float waitTime = 25f;                // ? Change this number in Inspector!

    [Header("Next scene name (you can change this too)")]
    public string nextSceneName = "MainScene";  // ? Change this in Inspector too!

    void Start()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        int index = 0;
        float timer = 0f;

        while (timer < waitTime)
        {
            loadingText.text = messages[index % messages.Length];
            index++;

            // Change message every ~4-5 seconds (you can tweak this too if you want)
            yield return new WaitForSeconds(4.5f);
            timer += 4.5f;
        }

        loadingText.text = "Welcome!";
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextSceneName);
    }

    // Call this from any button to start loading
    public static void StartGame(string sceneName, float customWaitTime = 25f)
    {
        targetScene = sceneName;
        targetWaitTime = customWaitTime;
        SceneManager.LoadScene("LoadingScene"); // your loading scene name
    }

    // So you can change scene and time from other scripts
    private static string targetScene;
    private static float targetWaitTime = 25f;

    void OnEnable()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            nextSceneName = targetScene;
            waitTime = targetWaitTime;
        }
    }
}