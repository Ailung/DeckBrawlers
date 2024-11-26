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
        Destroy(gameObject, GetAnimationLength());
        damagedEnemies = new List<GameObject>();

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
        if (collision.CompareTag("Enemy") && !damagedEnemies.Contains(collision.gameObject))
        {

            collision.GetComponent<HealthManager>().getDamage(GetSpellDamage());
            damagedEnemies.Add(gameObject);
        }
    }
}