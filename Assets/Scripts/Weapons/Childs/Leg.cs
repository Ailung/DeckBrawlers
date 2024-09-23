using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : Weapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    private CharacterController player;
    private bool isAttacking = false;
    public override int AttackDamage => attackDamage;

    public override int AttackSpeed => attackSpeed;

    public override CharacterController Player => player;
    public override bool IsAttacking => isAttacking;

    private void Awake()
    {
        player = this.gameObject.GetComponentInParent<CharacterController>();
    }
    public override void Attack()
    {
        isAttacking = true;
        gameObject.SetActive(true);
        StartCoroutine(waitToEnd());
    }

    IEnumerator waitToEnd()
    {
        yield return new WaitForSeconds(attackSpeed);
        gameObject.SetActive(false);
        isAttacking = false;
    }
}
