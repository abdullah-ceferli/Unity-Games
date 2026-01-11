using UnityEngine;
using UnityEngine.UI;

public class BoxOpener : MonoBehaviour
{
    public PlayerMoney playerMoney;      
    public GameObject boxPrefab;          
    public Transform boxSpawnPosition;   
    public int boxPrice = 300;             

    public void OnOpenBoxButtonClicked()
    {
        if (playerMoney.GetCoins() >= boxPrice)
        {
            playerMoney.SpendCoins(boxPrice);
            Instantiate(boxPrefab, boxSpawnPosition.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Ќедостаточно коинов дл€ открыти€ бокса!");
        }
    }
}
