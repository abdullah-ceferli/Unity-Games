using UnityEngine;

public class PlacementDetector : MonoBehaviour
{
    [Header("Settings")]
    public string blockTag = "Block";       // Tag your blocks with this
    public GameObject placementIndicator;   // Drag a semi-transparent prefab here
    public LayerMask whatIsBlock;           // Set this to the layer your blocks are on

    private GameObject currentIndicator;

    void Update()
    {
        // Detect input (Mouse click or single finger tap)
        if (Input.GetMouseButtonDown(0))
        {
            DetectBlockAndShowIndicator();
        }
    }

    void DetectBlockAndShowIndicator()
    {
        // Convert mouse position to a ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Shoot the ray
        if (Physics.Raycast(ray, out hit, 100f, whatIsBlock))
        {
            // Check if we actually hit a block with the correct tag
            if (hit.collider.CompareTag(blockTag))
            {
                // Calculate the position to place the new object
                // hit.point is exact impact; hit.normal is the direction the face is pointing
                // We add the normal to the center of the block to find the adjacent cell
                Vector3 validPosition = hit.collider.transform.position + hit.normal;

                // Optional: Snap to Grid (rounds to nearest whole number)
                validPosition = new Vector3(
                    Mathf.Round(validPosition.x),
                    Mathf.Round(validPosition.y),
                    Mathf.Round(validPosition.z)
                );

                ShowIndicator(validPosition);
            }
        }
        else
        {
            // If we clicked empty space, hide the indicator
            HideIndicator();
        }
    }

    void ShowIndicator(Vector3 pos)
    {
        // If we don't have an active indicator, spawn one
        if (currentIndicator == null)
        {
            currentIndicator = Instantiate(placementIndicator, pos, Quaternion.identity);
        }
        else
        {
            // Otherwise, just move the existing one
            currentIndicator.transform.position = pos;
            currentIndicator.SetActive(true);
        }
    }

    void HideIndicator()
    {
        if (currentIndicator != null)
        {
            currentIndicator.SetActive(false);
        }
    }
}