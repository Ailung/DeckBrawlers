using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpellControllerClass : MonoBehaviour
{
    public AreaSpellScriptableClass spellData;
    private float destroyAfterAnimation;
    private int spellDamage;
    List<GameObject> damagedEnemies;


    private void Start()
    {
        damagedEnemies = new List<GameObject>();
        Destroy(gameObject, GetAnimationLength());

    }

    public int GetSpellDamage()
    {
        return spellDamage = spellData.Damage;
    }
    public float GetAnimationLength()
    {
        return destroyAfterAnimation = spellData.AnimationClip.length;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !damagedEnemies.Contains(collision.gameObject) && spellData.Tag == "Player")
        {

            collision.GetComponent<HealthManager>().getDamage(GetSpellDamage());
            collision.GetComponent<EnemyController>().DamageStun();
            damagedEnemies.Add(gameObject);
        }
        if (collision.CompareTag("Player") && !damagedEnemies.Contains(collision.gameObject) && spellData.Tag == "Enemy")
        {
            collision.GetComponent<CharacterController>().DamageStun(GetSpellDamage());
            damagedEnemies.Add(gameObject);
        }
    }
}