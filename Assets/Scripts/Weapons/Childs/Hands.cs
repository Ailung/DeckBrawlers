using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    private CharacterController player;
    private EnemyController enemy;
    private bool isAttacking = false;
    public override int AttackDamage => attackDamage;

    public override int AttackSpeed => attackSpeed;

    public override CharacterController Player => player;
    public override bool IsAttacking => isAttacking;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        player = this.gameObject.GetComponentInParent<CharacterController>();
        enemy = this.gameObject.GetComponentInParent<EnemyController>();
    }
    public override void Attack(float agilityStat = 0)
    {
        if (!isAttacking) 
        {
            isAttacking = true;
            gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(waitToEnd(agilityStat));
        }
    }

    IEnumerator waitToEnd(float agilityStat)
    {
        yield return new WaitForSeconds((float)(attackSpeed * (agilityStat / 10) + attackSpeed));
        gameObject.SetActive(false);
        isAttacking = false;
    }
    
}
