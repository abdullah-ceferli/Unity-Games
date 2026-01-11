using UnityEngine;

public class CoinAndEmeraldSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject emeraldPrefab;

    public Transform[] startPoints;  // Массив начальных точек для всех дорог
    public Transform[] endPoints;    // Массив конечных точек для всех дорог

    public int coinsPerSpawn = 5;
    public int emeraldsPerSpawn = 3;

    public float coinSpawnInterval = 60f;   // Спавнить монеты каждые 60 секунд
    public float emeraldSpawnInterval = 180f; // Спавнить изумруды каждые 180 секунд

    private float coinTimer;
    private float emeraldTimer;

    void Update()
    {
        coinTimer += Time.deltaTime;
        emeraldTimer += Time.deltaTime;

        if (coinTimer >= coinSpawnInterval)
        {
            SpawnCoins();
            coinTimer = 0;
        }

        if (emeraldTimer >= emeraldSpawnInterval)
        {
            SpawnEmeralds();
            emeraldTimer = 0;
        }
    }

    // Спавним монеты для каждой дороги
    void SpawnCoins()
    {
        for (int i = 0; i < startPoints.Length; i++) // Для каждой дороги
        {
            for (int j = 0; j < coinsPerSpawn; j++)
            {
                float t = (float)j / (coinsPerSpawn - 1);  // Распределяем монеты равномерно по дороге
                Vector3 spawnPos = Vector3.Lerp(startPoints[i].position, endPoints[i].position, t);
                Instantiate(coinPrefab, spawnPos + Vector3.up * 0.5f, Quaternion.identity);
            }
        }
    }

    // Спавним изумруды для каждой дороги
    void SpawnEmeralds()
    {
        for (int i = 0; i < startPoints.Length; i++) // Для каждой дороги
        {
            for (int j = 0; j < emeraldsPerSpawn; j++)
            {
                float t = (float)j / (emeraldsPerSpawn - 1);  // Распределяем изумруды равномерно по дороге
                Vector3 spawnPos = Vector3.Lerp(startPoints[i].position, endPoints[i].position, t);
                Instantiate(emeraldPrefab, spawnPos + Vector3.up * 0.5f, Quaternion.identity);
            }
        }
    }
}
