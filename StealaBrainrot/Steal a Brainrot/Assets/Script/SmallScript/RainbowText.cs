using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RainbowText : MonoBehaviour
{
    public float speed = 1f;           // How fast the rainbow cycles
    public bool loopForever = true;

    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmpText.color = GetRainbowColor(Time.time * speed);
    }

    Color GetRainbowColor(float t)
    {
        float r = Mathf.Sin(t * 2f) * 0.5f + 0.5f;
        float g = Mathf.Sin(t * 2f + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(t * 2f + 4f) * 0.5f + 0.5f;
        return new Color(r, g, b, 1f);
    }

    // Bonus: Slower smooth rainbow (more beautiful)
    public static Color SmoothRainbow(float time)
    {
        return new Color(
            Mathf.Sin(time) * 0.5f + 0.5f,
            Mathf.Sin(time + Mathf.PI * 2 / 3) * 0.5f + 0.5f,
            Mathf.Sin(time + Mathf.PI * 4 / 3) * 0.5f + 0.5f,
            1f
        );
    }
}