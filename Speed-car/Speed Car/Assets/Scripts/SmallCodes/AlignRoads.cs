using UnityEngine;

public class AlignRoads : MonoBehaviour
{

    [ContextMenu("Align Roads")]
    public void Align()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            //child.localPosition = new Vector3(i * spacing, 0f, 0f);
        }
    }
}
