using UnityEngine;
using System.Collections.Generic;

public class ScatterNoOverlap : MonoBehaviour
{
    public Vector2 areaSize = new Vector2(100f, 100f); 
    public float minY = 0f;
    public float maxY = 0f;
    public float treeSpacing = 10f;

    private List<Vector3> usedPositions = new List<Vector3>();

    [ContextMenu("Scatter No Overlap")]
    void Scatter()
    {
        usedPositions.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tree = transform.GetChild(i);
            Vector3 pos;

            int attempts = 0;
            do
            {
                float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
                float z = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
                float y = Random.Range(minY, maxY);

                pos = new Vector3(x, y, z);
                attempts++;

                if (attempts > 1000)
                {
                    Debug.LogWarning("Too many trees or not enough space.");
                    return;
                }

            } while (IsTooClose(pos));

            usedPositions.Add(pos);
            tree.localPosition = pos;
        }
    }

    bool IsTooClose(Vector3 newPos)
    {
        foreach (Vector3 pos in usedPositions)
        {
            if (Vector3.Distance(pos, newPos) < treeSpacing)
                return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + Vector3.up * ((minY + maxY) / 2f), new Vector3(areaSize.x, 0.1f, areaSize.y));
    }
}
