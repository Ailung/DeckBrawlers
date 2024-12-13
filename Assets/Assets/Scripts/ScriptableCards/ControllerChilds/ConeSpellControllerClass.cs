using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpellControllerClass : MonoBehaviour
{
    public ConeSpellScriptableClass spellData;
    private float destroyAfterAnimation;
    private int spellDamage;
    List<GameObject> damagedEnemies;


    private void Start()
    {
        Destroy(gameObject, GetAnimationLength());
        damagedEnemies = new List<GameObject>();
        this.gameObject.GetComponent<Animation>().clip = spellData.animationClip;

    }

    public int GetSpellDamage()
    {
        return spellDamage = spellData.Damage;
    }
    public float GetAnimationLength()
    {
        Debug.Log(spellData.AnimationClip.length);
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
