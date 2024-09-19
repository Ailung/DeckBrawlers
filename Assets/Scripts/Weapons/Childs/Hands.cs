using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private CharacterController player;
    public override int AttackDamage => attackDamage;

    public override int AttackSpeed => attackSpeed;

    public override CharacterController Player => player;

    public override void Attack()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
