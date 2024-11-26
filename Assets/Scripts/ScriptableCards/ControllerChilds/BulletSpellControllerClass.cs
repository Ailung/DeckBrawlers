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
        transform.position += Vector3.right * GetSpellSpeed() * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && spellData.Tag == "Player")
        {
            collision.GetComponent<HealthManager>().getDamage(GetSpellDamage());
            this.gameObject.GetComponent<Bullet>().Deactivate();

        }
        if (collision.CompareTag("player") && spellData.Tag == "Enemy")
        {
            collision.GetComponent<HealthManager>().getDamage(GetSpellDamage());
            this.gameObject.GetComponent<Bullet>().Deactivate();
        }
    }
}
