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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    gameObject.SetActive(false);
    //}
    public override void Attack(float agilityStat = 0)
    {
        isAttacking = true;
        gameObject.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(waitToEnd(agilityStat));
    }

    IEnumerator waitToEnd(float agilityStat)
    {
        yield return new WaitForSeconds((float)(attackSpeed * (agilityStat/10) + attackSpeed));
        gameObject.SetActive(false);
        isAttacking = false;
    }
}
