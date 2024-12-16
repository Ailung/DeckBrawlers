using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    BulletPool bulletPool;
    [SerializeField] Bullet bulletPrefab;
    GameObject player;

    private void Awake()
    {
        bulletPool = new BulletPool(bulletPrefab);
        player = FindObjectOfType<CharacterController>().gameObject;
        InvokeRepeating("FindOutOfBoundsBullets", 0.1f, 0.1f);
    }

    public Bullet GetBullet()
    {
        return bulletPool.bulletPool.Get();
    }
    private void FindOutOfBoundsBullets()
    {
        foreach(Bullet bullet in FindObjectsOfType<Bullet>())
        {
            
            float bulletDistance = Vector2.Distance(bullet.transform.position, player.transform.position);
            if (bulletDistance > 20)
            {
                bulletPool.bulletPool.Release(bullet);
            }
        }
    }
}
