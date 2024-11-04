using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;

    public int health;

    public int MaxHealth => maxHealth;

    private void Awake()
    {
        health = maxHealth;
        if (TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            //GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().fillAmount = (float)health / 100f;
        }
    }

    public void getDamage(int damage)
    {
        health -= damage;

        if (TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            //GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().fillAmount = (float)health /100f;
        }

        if (health <= 0)
        {
            if (TryGetComponent<CharacterController>(out CharacterController characterController2)) 
            {
                GameManager.Instance.ChangeScene("Lose");
            } else if (TryGetComponent<EnemyController>(out EnemyController enemy))
            {
                enemy.OnDie();
                Destroy(gameObject);
            }
        }
    }

    public void getHeal(int heal)
    {
        if (health < maxHealth)
        {
            health += heal;
            if (TryGetComponent<CharacterController>(out CharacterController characterController))
            {
                //GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().fillAmount = (float)health / 100f;
            }
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public void Initialize(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }
}
