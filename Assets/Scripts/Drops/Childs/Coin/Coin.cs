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
    public override string Id => id;

    public override void PickUp()
    {
        Destroy(gameObject);

        Debug.Log("Agarrar Coin");
    }
}
