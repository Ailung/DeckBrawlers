using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemySriptableClass[] Enemies;
    private Dictionary<string, EnemySriptableClass> idEnemies;
    [SerializeField] private GameObject enemyPrefab;

    private void Awake()
    {
        idEnemies = new Dictionary<string, EnemySriptableClass>();

        foreach (var enemy in Enemies)
        {
            idEnemies.Add(enemy.id, enemy);
        }
    }

    public GameObject Create(string id)
    {
        if (!idEnemies.TryGetValue(id, out EnemySriptableClass enemyType))
        {
            return null;
        }

        enemyPrefab.GetComponent<EnemyController>().changeEnemyData(enemyType);
        return Instantiate(enemyPrefab);
    }
}
