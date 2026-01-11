using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;   
    public float xSpawn = 9124;
    public float tileLength = 124;        
    public int numberOfTiles = 3;        
    public float spawnTriggerDistance = 70;

    public Transform playerTranform;

    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTranform == null) return;

        if (playerTranform.position.x - spawnTriggerDistance > xSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.right * xSpawn, transform.rotation);
        activeTiles.Add(go);
        xSpawn += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
