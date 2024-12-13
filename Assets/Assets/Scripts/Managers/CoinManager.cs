using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins;

    public int Coins => coins;

    private void Awake()
    {
        coins = 0;
    }

    public void getCoins(int numberCoins)
    {
        coins += numberCoins;
    }
}
