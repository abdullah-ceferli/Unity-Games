using UnityEngine;

public class MobileInput : MonoBehaviour
{
    [Header("Joystick UI")]
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    public float joystickRadius = 80f;

    [HideInInspector] public Vector2 Move;
    [HideInInspector] public Vector2 Look;

    private int moveFinger = -1;
    private int lookFinger = -1;
    private Vector2 startPos;

    void Start()
    {
        joystickBG.gameObject.SetActive(false);
    }

    void Update()
    {
        Move = Vector2.zero;
        Look = Vector2.zero;

        foreach (Touch t in Input.touches)
        {
            // LEFT SIDE = MOVE
            if (t.position.x < Screen.width * 0.5f)
            {
                if (t.phase == TouchPhase.Began && moveFinger == -1)
                {
                    moveFinger = t.fingerId;
                    startPos = t.position;
                    joystickBG.position = startPos;
                    joystickHandle.position = startPos;
                    joystickBG.gameObject.SetActive(true);
                }

                if (t.fingerId == moveFinger)
                {
                    Vector2 delta = t.position - startPos;
                    delta = Vector2.ClampMagnitude(delta, joystickRadius);
                    joystickHandle.position = startPos + delta;
                    Move = delta / joystickRadius;

                    if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                    {
                        moveFinger = -1;
                        joystickBG.gameObject.SetActive(false);
                    }
                }
            }
            // RIGHT SIDE = LOOK
            else
            {
                if (t.phase == TouchPhase.Began && lookFinger == -1)
                    lookFinger = t.fingerId;

                if (t.fingerId == lookFinger)
                {
                    Look = t.deltaPosition;

                    if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                        lookFinger = -1;
                }
            }
        }
    }
}
