using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Drops
{
    [SerializeField] private string id;
    [SerializeField] private FoodScriptable data;
    private HealthManager manager;
    private CharacterController player;
    public override string Id => id;

    private void Awake()
    {
        player = FindFirstObjectByType<CharacterController>();
        manager = player.GetComponent<HealthManager>();
    }

    public override void PickUp()
    {
        Destroy(gameObject);
        manager.getHeal(data.foodAmount);
    }
}
