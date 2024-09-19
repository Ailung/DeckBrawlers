using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Drops
{
    [SerializeField] private string id;
    [SerializeField] private FoodEnum type;
    public override string Id => id;

    public override void PickUp()
    {
        Destroy(gameObject);

        Debug.Log("regenerar vida");
    }
}
