using UnityEngine;

[ExecuteAlways]
public class CubeLineGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 5;
    public float gap = 2f;

    private void OnValidate()
    {
        if (cubePrefab == null) return;

        Generate();
    }

    void Generate()
    {
        // Delete old children
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        // Create cubes
        for (int i = 0; i < cubeCount; i++)
        {
            GameObject cube = Instantiate(cubePrefab, transform);
            cube.transform.localPosition = new Vector3(i * gap, 0, 0);
        }
    }
}
