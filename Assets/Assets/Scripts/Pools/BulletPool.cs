using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public ObjectPool<Bullet> bulletPool;
    [SerializeField] private Bullet bulletPrefab;
    public BulletPool(Bullet bullet)
    {
        bulletPrefab = bullet;
        bulletPool = new ObjectPool<Bullet>(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 1000);
    }
    private void Awake()
    {

    }

    private Bullet CreatePoolItem()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.gameObject.SetActive(false);
        bullet.pool = bulletPool;
        return bullet;
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
