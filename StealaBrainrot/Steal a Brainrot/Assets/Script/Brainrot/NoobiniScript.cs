using System.Collections.Generic;
using UnityEngine;
using TMPro; // If using TextMeshPro, otherwise use UnityEngine.UI;

public class NoobiniScript : MonoBehaviour
{
    // Points for movement from A to B and back
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;

    // Player reference for proximity check
    public GameObject player;
    public float deleteDistance = 5f;
    public float colorChangeDistance = 10f;

    // Renderer for changing color (assign in Inspector, e.g., a button or the main mesh renderer)
    public Renderer objectRenderer;
    public Color normalColor = Color.white;
    public Color closeColor = Color.red;

    // Timers in seconds (5 min = 300s, 15 min = 900s)
    public float timer5min = 300f;
    public float timer15min = 900f;

    // UI Text for countdowns (assign in Inspector - use TextMeshProUGUI or UnityEngine.UI.Text)
    public TextMeshProUGUI timer5Text; // Or public UnityEngine.UI.Text timer5Text;
    public TextMeshProUGUI timer15Text; // Or public UnityEngine.UI.Text timer15Text;

    // Lists for brainrot prefabs and their spawn chances (user to fill in Inspector or code)
    // Chances should sum to a total (e.g., 100 or 1.0) - example: 99% common, 0.5% rare, 0.5% epic
    // Add your rare, epic, and other rarity brainrots here with corresponding chances
    public List<GameObject> brainrots5min = new List<GameObject>(); // List for 5min spawns
    public List<float> chances5min = new List<float>(); // e.g., {99f, 0.5f, 0.5f} for percentages or {0.99f, 0.005f, 0.005f} for probabilities

    public List<GameObject> brainrots15min = new List<GameObject>(); // List for 15min spawns (other types)
    public List<float> chances15min = new List<float>(); // Similar to above

    private bool movingToB = true;

    void Update()
    {
        // Movement: Ping-pong between A and B
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            movingToB = !movingToB;
        }

        // Check player distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Change color if player is close
        if (distanceToPlayer < colorChangeDistance)
        {
            objectRenderer.material.color = closeColor;
        }
        else
        {
            objectRenderer.material.color = normalColor;
        }

        // Delete if player is too close
        if (distanceToPlayer < deleteDistance)
        {
            Destroy(gameObject);
        }

        // Update timers
        timer5min -= Time.deltaTime;
        UpdateTimerText(timer5Text, timer5min);
        if (timer5min <= 0)
        {
            SpawnBrainrot(brainrots5min, chances5min);
            timer5min = 300f; // Reset to 5 min
        }

        timer15min -= Time.deltaTime;
        UpdateTimerText(timer15Text, timer15min);
        if (timer15min <= 0)
        {
            SpawnBrainrot(brainrots15min, chances15min);
            timer15min = 900f; // Reset to 15 min
        }
    }

    // Function to update timer text (formats seconds to MM:SS)
    private void UpdateTimerText(TextMeshProUGUI textObj, float timeLeft) // Or UnityEngine.UI.Text
    {
        if (textObj != null)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            textObj.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Function to spawn a random brainrot based on chances
    private void SpawnBrainrot(List<GameObject> brainrotList, List<float> chanceList)
    {
        if (brainrotList.Count == 0 || chanceList.Count != brainrotList.Count)
        {
            Debug.LogWarning("Brainrot list or chances not set properly.");
            return;
        }

        // Calculate total chance (works for percentages or probabilities)
        float totalChance = 0f;
        foreach (float chance in chanceList)
        {
            totalChance += chance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulative = 0f;

        for (int i = 0; i < brainrotList.Count; i++)
        {
            cumulative += chanceList[i];
            if (randomValue < cumulative)
            {
                // Spawn at current position (or adjust as needed)
                Instantiate(brainrotList[i], transform.position, Quaternion.identity);
                return;
            }
        }
    }
}