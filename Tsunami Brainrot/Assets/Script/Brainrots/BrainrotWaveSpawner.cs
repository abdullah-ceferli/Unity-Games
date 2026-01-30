using UnityEngine;
using System.Collections;

public class BrainrotSteadySpawner : MonoBehaviour
{
    [Header("Brainrot Prefabs")]
    public GameObject[] brainrotPrefabs; // Assign your brainrot prefabs here (drag multiple into the array)

    [Header("Spawn Plane (Recommended - Auto Detects Size/Position)")]
    public Transform spawnPlane; // Drag your Plane GameObject here - automatically uses its bounds!

    [Header("Manual Spawn Settings (Used if Spawn Plane not assigned)")]
    public Vector2 planeCenterXZ = Vector2.zero; // Center of the spawn plane (X,Z)
    public float planeY = 0f; // Height (Y) of the spawn plane
    public Vector2 planeSizeXZ = new Vector2(10f, 10f); // Size of the spawn area on the plane (X,Z extents) - Default for Unity Plane!

    [Header("Wave Settings")]
    public int numberToSpawn = 4; // How many to spawn initially and keep alive (configurable!)
    public float respawnInterval = 60f; // Time in seconds between full wave replacements

    [Header("Replace Settings")]
    public float replaceDelay = 1f; // Delay (seconds) between destroying/spawning each one in a wave

    [Header("Spawn Offset")]
    public float heightOffset = 0.01f; // Small offset above plane to avoid clipping/z-fighting

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

        // Get spawn position
        Vector3 spawnPos = GetSpawnPosition();

        // Random Y rotation
        Quaternion spawnRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        // Instantiate as child of spawnParent
        Instantiate(prefab, spawnPos, spawnRot, spawnParent);
    }

    Vector3 GetSpawnPosition()
    {
        if (spawnPlane != null)
        {
            // Auto-detect using plane's world bounds (handles position, scale, etc.)
            MeshRenderer renderer = spawnPlane.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                return new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    bounds.max.y + heightOffset,
                    Random.Range(bounds.min.z, bounds.max.z)
                );
            }
        }

        // Fallback to manual settings
        return new Vector3(
            planeCenterXZ.x + Random.Range(-planeSizeXZ.x / 2f, planeSizeXZ.x / 2f),
            planeY + heightOffset,
            planeCenterXZ.y + Random.Range(-planeSizeXZ.y / 2f, planeSizeXZ.y / 2f)
        );
    }
}