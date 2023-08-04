using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsCountWidget;
    private int coinAmount = 0;
    
    public void CollectCoins(int amount)
    {
        coinAmount += amount;
        coinsCountWidget.SetText(coinAmount.ToString());
    }
}