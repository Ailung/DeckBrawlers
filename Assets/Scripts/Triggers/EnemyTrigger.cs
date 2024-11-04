using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] string[] enemySriptableClasses;
    [SerializeField] EnemyFactory enemyFactory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (string enemyId in enemySriptableClasses)
            {
                enemyFactory.Create(enemyId);
            }

            this.gameObject.SetActive(false);
        }
    }
}
