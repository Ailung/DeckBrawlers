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
        InvokeRepeating("FindOutOfBoundsBullets", 1f, 1f);
    }

    public Bullet GetBullet()
    {
        return bulletPool.bulletPool.Get();
    }
    private void FindOutOfBoundsBullets()
    {
        foreach(Bullet bullet in FindObjectsOfType<Bullet>())
        {
            Vector2 normalized = bullet.transform.position - player.transform.position;
            if (normalized.magnitude > 1000)
            {
                bulletPool.bulletPool.Release(bullet);
            }
        }
    }
}
