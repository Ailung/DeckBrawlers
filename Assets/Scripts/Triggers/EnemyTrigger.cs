using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] string[] enemySriptableClasses;
    [SerializeField] EnemyFactory enemyFactory;
    [SerializeField] GameObject spawnPoint;
    private System.Random rnd = new System.Random();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (string enemyId in enemySriptableClasses)
            {
                enemyFactory.Create(enemyId, spawnPoint.transform.position + new Vector3(0,rnd.Next(-1 , 1),0));
            }

            this.gameObject.SetActive(false);
        }
    }
}
