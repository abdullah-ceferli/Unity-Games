using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        Time.timeScale = 1f; // reset pause
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1f;
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
