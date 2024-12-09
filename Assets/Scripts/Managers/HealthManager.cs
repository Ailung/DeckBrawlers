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

    private EnemyController EController;
    private CharacterController CController;

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
        AudioManager.Instance.PlaySFX("Hit");

        if (TryGetComponent<CharacterController>(out CharacterController characterController))
        {
            //GameObject.Find("Panel").GetComponent<UnityEngine.UI.Image>().fillAmount = (float)health /100f;
        }

        if (health <= 0)
        {
            if (TryGetComponent<CharacterController>(out CharacterController characterController2)) 
            {
                CController = characterController2;
                StartCoroutine(PlayerDead());
            } else if (TryGetComponent<EnemyController>(out EnemyController enemy))
            {
                EController = enemy;
                StartCoroutine(EnemyDead());
            }
        }
    }

    private IEnumerator PlayerDead()
    {
        if (CController.CurrentAnimation != 9)
        {
            CController.Animation(9);
        }
        yield return new WaitForSeconds(0.35f);
        GameManager.Instance.ChangeScene("Lose");
    }
    private IEnumerator EnemyDead()
    {
        if (EController.CurrentAnimation != 9)
        {
            EController.Animation(9);
        }
        yield return new WaitForSeconds(0.35f);
        EController.OnDie();
        Destroy(gameObject);
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
