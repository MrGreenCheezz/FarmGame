using TMPro;
using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = FarmInstance.instance.LocalPlayerData.money.ToString();
        FarmInstance.OnMoneyChanged += UpdateMoney;
    }

    public void UpdateMoney()
    {
        moneyText.text = FarmInstance.instance.LocalPlayerData.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
