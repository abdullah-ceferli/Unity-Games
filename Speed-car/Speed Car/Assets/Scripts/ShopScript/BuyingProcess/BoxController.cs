using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public Button boxButton;             
    public Image boxImage;               
    public ParticleSystem breakEffect;   
    public AudioSource breakSound;       
    public Transform rewardPanel;        
    public ParticleSystem clickLightEffect; 

    public GameObject coinRewardPrefab;   
    public GameObject gemRewardPrefab;    

    public PlayerMoney playerMoney;
    private int clicksRemaining = 3; 

    public void Initialize(PlayerMoney pm)
    {
        playerMoney = pm;
        boxButton.onClick.AddListener(OnBoxClick); 
    }

    public void OnBoxClick()
    {
        clicksRemaining--;
        if (clicksRemaining > 0)
        {
            // Ёффект удара
            breakEffect.Play();
            breakSound.Play();
            clickLightEffect.Play();
        }
        else
        {
            StartCoroutine(OpenBox());
        }
    }

    // ћетод дл€ открыти€ бокса
    private IEnumerator OpenBox()
    {
        boxButton.interactable = false;

        // ‘инальный взрыв
        breakEffect.Play();
        breakSound.Play();
        boxImage.enabled = false; 

        yield return new WaitForSeconds(0.5f);

        GenerateRewards();
        yield return new WaitForSeconds(2f);

        Destroy(gameObject); 
    }

    private void GenerateRewards()
    {
        int coins = GetCoinReward();
        playerMoney.AddCoins(coins);
        ShowReward(coinRewardPrefab, coins);

        int gems = GetGemReward();
        playerMoney.AddGems(gems);
        ShowReward(gemRewardPrefab, gems);
    }

    private int GetCoinReward()
    {
        float r = Random.Range(0f, 100f);
        if (r < 50f) return 200;   
        if (r < 74f) return 250;  
        if (r < 75f) return 500;   
        return 200; 
    }

    private int GetGemReward()
    {
        float r = Random.Range(0f, 100f);
        if (r < 50f) return 15;    
        if (r < 74f) return 25;    
        if (r < 75f) return 50;    
        return 15;
    }

    // ќтображаем награду на экране
    private void ShowReward(GameObject rewardPrefab, int amount)
    {
        GameObject reward = Instantiate(rewardPrefab, rewardPanel);
        reward.GetComponentInChildren<Text>().text = amount.ToString();
    }
}
