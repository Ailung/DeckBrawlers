using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] string[] enemyScriptableClasses;
    [SerializeField] string[] enemyData;
    [SerializeField] EnemyFactory enemyFactory;
    [SerializeField] GameObject spawnPoint;
    private System.Random rnd = new System.Random();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemyData.Length == enemyScriptableClasses.Length) 
            {
                
                for (int i = 0; i < enemyScriptableClasses.Length; i++)
                {
                    enemyFactory.Create(enemyScriptableClasses.GetValue(i).ToString(), spawnPoint.transform.position + new Vector3(0, rnd.Next(-1, 1), 0), enemyData.GetValue(i).ToString());
                }

                this.gameObject.SetActive(false);
            }
        }
    }
}
