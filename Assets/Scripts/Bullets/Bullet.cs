using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float lifeTime = 5f; //Vida bola
    private float currentTime;
    public ObjectPool<Bullet> pool;
    //public AudioClip sonido;
    //private AudioSource audioSource;

    [SerializeField] float lerpSpeedRotation;
    [SerializeField] public int damage;

    private void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
    }

    private void Start()
    {

    }

    public void Deactivate()
    {
        pool.Release(this);
    }
}
