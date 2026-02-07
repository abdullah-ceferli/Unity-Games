using UnityEngine;
using System.Collections;
using UnityEngine.AI; // For NavMesh

public class BrainrotSteadySpawner : MonoBehaviour
{
    [Header("Brainrot Prefabs")]
    public GameObject[] brainrotPrefabs; // Assign your brainrot prefabs here (drag multiple into the array)
    [Header("Spawn Settings")]
    public int numberToSpawn = 4; // How many to spawn initially and keep alive (configurable!)
    public float respawnInterval = 60f; // Time in seconds between full wave replacements
    [Header("Replace Settings")]
    public float replaceDelay = 1f; // Delay (seconds) between destroying/spawning each one in a wave
    [Header("Spawn Offset")]
    public float heightOffset = 0.01f; // Small offset above surface to avoid clipping/z-fighting
    [Header("NavMesh Settings")]
    public float navMeshSampleRadius = 5f; // Max distance to search for NavMesh surface if needed
    public int maxSampleTries = 10; // Max attempts to find a valid NavMesh position
    public Transform spawnParent; // Optional: Assign an empty GameObject as parent for spawned brainrots

    void Start()
    {
        if (spawnParent == null)
        {
            GameObject parentObj = new GameObject("BrainrotParent");
            spawnParent = parentObj.transform;
            spawnParent.parent = transform;
        }
        // Initial spawn: all at once
        SpawnInitial();
        // Start the wave replacement loop
        StartCoroutine(WaveReplacementRoutine());
    }

    void SpawnInitial()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnSingle();
        }
    }

    IEnumerator WaveReplacementRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnInterval);
            // Replace all one by one
            yield return StartCoroutine(ReplaceWaveOneByOne());
        }
    }

    IEnumerator ReplaceWaveOneByOne()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            ReplaceOne();
            yield return new WaitForSeconds(replaceDelay);
        }
    }

    void ReplaceOne()
    {
        // Destroy one random existing brainrot
        if (spawnParent.childCount > 0)
        {
            int randomIndex = Random.Range(0, spawnParent.childCount);
            Destroy(spawnParent.GetChild(randomIndex).gameObject);
        }
        // Immediately spawn a new one to keep count constant
        SpawnSingle();
    }

    void SpawnSingle()
    {
        if (brainrotPrefabs.Length == 0) return;
        // Pick random prefab
        GameObject prefab = brainrotPrefabs[Random.Range(0, brainrotPrefabs.Length)];
        // Get random spawn position on the entire NavMesh
        Vector3 spawnPos = GetRandomNavMeshPosition();
        // Random Y rotation
        Quaternion spawnRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        // Instantiate as child of spawnParent
        Instantiate(prefab, spawnPos, spawnRot, spawnParent);
    }

    Vector3 GetRandomNavMeshPosition()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        if (triangulation.areas.Length == 0)
        {
            Debug.LogError("No NavMesh data available!");
            return Vector3.zero; // Fallback to origin or handle error
        }

        // Pick a random triangle weighted by area for uniform distribution
        float[] areas = new float[triangulation.indices.Length / 3];
        for (int i = 0; i < areas.Length; i++)
        {
            int idx = i * 3;
            Vector3 a = triangulation.vertices[triangulation.indices[idx]];
            Vector3 b = triangulation.vertices[triangulation.indices[idx + 1]];
            Vector3 c = triangulation.vertices[triangulation.indices[idx + 2]];
            areas[i] = Vector3.Cross(b - a, c - a).magnitude / 2f;
        }

        float totalArea = 0f;
        foreach (float area in areas) totalArea += area;

        float randomArea = Random.Range(0f, totalArea);
        int selectedTriangle = -1;
        float accumulated = 0f;
        for (int i = 0; i < areas.Length; i++)
        {
            accumulated += areas[i];
            if (randomArea <= accumulated)
            {
                selectedTriangle = i;
                break;
            }
        }

        // Get vertices of the selected triangle
        int baseIdx = selectedTriangle * 3;
        Vector3 v0 = triangulation.vertices[triangulation.indices[baseIdx]];
        Vector3 v1 = triangulation.vertices[triangulation.indices[baseIdx + 1]];
        Vector3 v2 = triangulation.vertices[triangulation.indices[baseIdx + 2]];

        // Barycentric coordinates for random point in triangle
        float r1 = Random.Range(0f, 1f);
        float r2 = Random.Range(0f, 1f);
        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }
        float r3 = 1f - r1 - r2;

        Vector3 randomPos = v0 * r3 + v1 * r1 + v2 * r2;

        // Sample to ensure it's on NavMesh (should be, but just in case)
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            return hit.position + Vector3.up * heightOffset;
        }

        // If failed (unlikely), retry with simple sampling fallback
        Debug.LogWarning("Triangle sample failed NavMesh check. Falling back.");
        for (int tryCount = 0; tryCount < maxSampleTries; tryCount++)
        {
            // Pick a random vertex as starting point and sample around it
            Vector3 startPos = triangulation.vertices[Random.Range(0, triangulation.vertices.Length)];
            if (NavMesh.SamplePosition(startPos, out hit, navMeshSampleRadius, NavMesh.AllAreas))
            {
                return hit.position + Vector3.up * heightOffset;
            }
        }

        Debug.LogError("Could not find valid NavMesh position!");
        return Vector3.zero; // Error fallback
    }
}