using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    // Основная валюта: монеты (coins)
    public int coins = 0;

    // Самоцветы (gems)
    public int gems = 0;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gemsText;

    void Start()
    {
        // Загружаем сохранённые данные
        coins = PlayerPrefs.GetInt("Coins", 0);
        gems = PlayerPrefs.GetInt("Gems", 0);
        UpdateUI();
    }

    // Добавить монеты
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
        PlayerPrefs.SetInt("Coins", coins);
    }

    // Тратить монеты
    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            PlayerPrefs.SetInt("Coins", coins);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Получить сколько монет
    public int GetCoins()
    {
        return coins;
    }

    // Добавить самоцветы
    public void AddGems(int amount)
    {
        gems += amount;
        UpdateUI();
        PlayerPrefs.SetInt("Gems", gems);
    }

    // Тратить самоцветы (если надо)
    public bool SpendGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            UpdateUI();
            PlayerPrefs.SetInt("Gems", gems);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Обновляем интерфейс
    void UpdateUI()
    {
        if (coinsText != null)
            coinsText.text = "Coins: " + coins.ToString();

        if (gemsText != null)
            gemsText.text = "Gems: " + gems.ToString();
    }
}
