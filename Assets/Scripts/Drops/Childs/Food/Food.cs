using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Drops
{
    [SerializeField] private string id;
    [SerializeField] private FoodScriptable data;
    [SerializeField] private HealthManager manager;
    public override string Id => id;

    public override void PickUp()
    {
        Destroy(gameObject);
        manager.getHeal(data.foodAmount);
        Debug.Log("regenerar vida");
    }
}
