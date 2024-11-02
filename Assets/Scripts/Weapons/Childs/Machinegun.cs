using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Machinegun : MonoBehaviour, IWeapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int bulletQuantity = 30;
    [SerializeField] private float attackSpeed;

    private CharacterController player;
    private CloseEnemy enemy;
    private bool isAttacking = false;
    private float timerShoot;
    private BulletPoolManager bulletPool;
    public int AttackDamage => attackDamage;
    public int BulletQuantity => bulletQuantity;

    public float AttackSpeed => attackSpeed;

    public CharacterController Player => player;
    public bool IsAttacking => isAttacking;



    private void Awake()
    {
        bulletPool = FindFirstObjectByType<BulletPoolManager>();
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            Bullet bullet = bulletPool.GetBullet();

            bullet.tag = "PlayerBullets";

            bullet.damage = attackDamage;

            bullet.GetComponent<Rigidbody2D>().velocity = gameObject.transform.right * gameObject.GetComponentInParent<Transform>(false).lossyScale.x * 20;

            bullet.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            
            isAttacking = true;

            timerShoot = 0;

            bulletQuantity--;
        }
    }

    public void Update()
    {
        if (isAttacking && timerShoot < attackSpeed)
        {
            timerShoot += Time.deltaTime;
        }

        if (timerShoot >= attackSpeed)
        {
            isAttacking = false;
        }
    }
}
