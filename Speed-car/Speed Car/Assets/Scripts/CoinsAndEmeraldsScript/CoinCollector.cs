using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public PlayerMoney playerMoney;  // reference to your PlayerMoney component

    private void OnTriggerEnter(Collider other)
    {
        // if you hit a coin pickup
        if (other.CompareTag("Coin"))
        {
            playerMoney.AddCoins(10);    // give 10 coins
            Destroy(other.gameObject);   // remove the pickup
        }

        // if you hit a gem pickup
        if (other.CompareTag("Gem"))
        {
            playerMoney.AddGems(5);      // give 5 gems
            Destroy(other.gameObject);
        }
    }
}
