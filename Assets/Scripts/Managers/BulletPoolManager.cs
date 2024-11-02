using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    BulletPool bulletPool;
    [SerializeField] Bullet bulletPrefab;

    private void Awake()
    {
        bulletPool = new BulletPool(bulletPrefab);
    }

    public Bullet GetBullet()
    {
        return bulletPool.bulletPool.Get();
    }
}
