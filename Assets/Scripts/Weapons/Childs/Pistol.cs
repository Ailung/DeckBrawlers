using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private int attackDamage;
    private int bulletQuantity = 1;
    [SerializeField] private float attackSpeed;

    private CharacterController player;
    private CloseEnemy enemy;
    private bool isAttacking = false;
    private float timerShoot;
    private BulletPoolManager bulletPool;
    public int AttackDamage => attackDamage;

    public float AttackSpeed => attackSpeed;

    public int BulletQuantity => bulletQuantity;

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

            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


            bullet.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * gameObject.GetComponentInParent<Transform>(false).lossyScale.x * 20, ForceMode2D.Impulse);

            isAttacking = true;

            timerShoot = 0;
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
