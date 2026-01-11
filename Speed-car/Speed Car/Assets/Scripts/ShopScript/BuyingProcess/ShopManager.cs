using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public PlayerMoney playerMoney;

    // Методы для покупки монет
    public void Buy500Coins() { playerMoney.AddCoins(500); }
    public void Buy1000Coins() { playerMoney.AddCoins(1000); }
    public void Buy2000Coins() { playerMoney.AddCoins(2000); }

    // Методы для покупки самоцветов
    public void Buy100Gems() { playerMoney.AddGems(100); }
    public void Buy250Gems() { playerMoney.AddGems(250); }
    public void Buy300Gems() { playerMoney.AddGems(300); }

    public void OpenShop() { shopPanel.SetActive(true); }
    public void CloseShop() { shopPanel.SetActive(false); }
}
