using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpellControllerClass : MonoBehaviour
{
    public BulletSpellScriptableClass spellData;
    private int spellDamage;
    private float spellSpeed;

    public int GetSpellDamage()
    {
        return spellDamage = spellData.Damage;
    }
    public float GetSpellSpeed()
    {
        return spellSpeed = spellData.Speed;
    }

    private void Update()
    {
        if (spellData.Tag == "Player") {
            transform.position += Vector3.right * GetSpellSpeed() * Time.deltaTime;
        }

        if (spellData.Tag == "Enemy") {
            transform.position += Vector3.left * GetSpellSpeed() * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && spellData.Tag == "Player")
        {
            collision.GetComponent<HealthManager>().getDamage(GetSpellDamage());
            collision.GetComponent<EnemyController>().DamageStun();
            this.gameObject.GetComponent<Bullet>().Deactivate();

        }
        if (collision.CompareTag("Player") && spellData.Tag == "Enemy")
        {
            collision.GetComponent<CharacterController>().DamageStun(GetSpellDamage());
            this.gameObject.GetComponent<Bullet>().Deactivate();
        }
    }
}
