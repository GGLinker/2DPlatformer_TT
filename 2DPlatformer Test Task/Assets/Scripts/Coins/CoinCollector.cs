using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int coinAmount = 0;
    
    public void CollectCoins(int amount)
    {
        coinAmount += amount;
    }
}