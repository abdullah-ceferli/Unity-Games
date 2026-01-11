using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject tree1Prefab;
    public GameObject tree2Prefab;
    public GameObject tree3Prefab;

    public Transform spawnPoint;
    public int playerCoins = 250;

    public void SpawnTree1()
    {
        Instantiate(tree1Prefab, spawnPoint.position, Quaternion.identity);
    }

    public void SpawnTree2()
    {
        Instantiate(tree2Prefab, spawnPoint.position, Quaternion.identity);
    }

    public void SpawnTree3WithCoins()
    {
        if (playerCoins >= 200)
        {
            playerCoins -= 200;
            Instantiate(tree3Prefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Tree 3 spawned, 200 coins spent!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
