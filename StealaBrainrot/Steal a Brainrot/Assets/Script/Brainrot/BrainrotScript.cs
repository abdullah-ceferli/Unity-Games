using UnityEngine;

public class BrainrotScript : MonoBehaviour
{
    public float dragSpeed = 3f;
    public Vector3 moveDirection = Vector3.forward; // Set direction in Inspector or randomize in Start

    void Start()
    {
        // Optional: Randomize direction if needed
        // moveDirection = Random.insideUnitSphere.normalized;
    }

    void Update()
    {
        // Move straight as if dragged (simple translation)
        transform.Translate(moveDirection * dragSpeed * Time.deltaTime);
    }
}