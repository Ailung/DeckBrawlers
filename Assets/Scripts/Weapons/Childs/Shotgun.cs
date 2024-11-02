using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shotgun : MonoBehaviour,IWeapon
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
            Bullet bullet2 = bulletPool.GetBullet();
            Bullet bullet3 = bulletPool.GetBullet();

            bullet.tag = "PlayerBullets";
            bullet2.tag = "PlayerBullets";
            bullet3.tag = "PlayerBullets";

            bullet.damage = attackDamage;
            bullet2.damage = attackDamage;
            bullet3.damage = attackDamage;

            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bullet2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bullet3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            bullet.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * gameObject.GetComponentInParent<Transform>(false).lossyScale.x * 20, ForceMode2D.Impulse);
            bullet2.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, 10) * gameObject.transform.right * gameObject.GetComponentInParent<Transform>(false).lossyScale.x * 20, ForceMode2D.Impulse);
            bullet3.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, -10) * gameObject.transform.right * gameObject.GetComponentInParent<Transform>(false).lossyScale.x * 20, ForceMode2D.Impulse);

            bullet.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            bullet2.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);
            bullet3.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);

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
