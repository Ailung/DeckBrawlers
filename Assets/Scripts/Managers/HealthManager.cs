using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;

    public int health;

    public int MaxHealth => maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }

    public void getDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void getHeal(int heal)
    {
        health += heal;
    }
}
