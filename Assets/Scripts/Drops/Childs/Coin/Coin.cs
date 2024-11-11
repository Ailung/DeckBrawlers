using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType
{
    small = 0,
    medium = 1,
    large = 2,
}

public class Coin : Drops
{
    [SerializeField] private string id;
    [SerializeField] private CoinScriptable data;
    [SerializeField] private CoinManager manager;
    public override string Id => id;

    private void Awake()
    {
        manager = FindFirstObjectByType<CoinManager>();
    }
    public override void PickUp()
    {
        Destroy(gameObject);
        manager.getCoins(data.coinAmount);

        Debug.Log("Agarrar Coin");
    }
}
