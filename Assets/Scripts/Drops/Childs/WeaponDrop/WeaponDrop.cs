using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : Drops
{
    [SerializeField] private string id;
    [SerializeField] private WeaponDropScriptable data;
    private CharacterController characterController;
    private IWeapon weapon;
    public override string Id => id;

    private void Awake()
    {
        characterController = FindFirstObjectByType<CharacterController>();
    }

    public override void PickUp()
    {
        Destroy(this.gameObject);
    }
}
