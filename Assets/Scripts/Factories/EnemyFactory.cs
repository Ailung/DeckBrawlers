using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private CloseEnemy[] Enemies;
    private Dictionary<string, CloseEnemy> idEnemies;

    private void Awake()
    {
        idEnemies = new Dictionary<string, CloseEnemy>();

        foreach (var enemy in Enemies)
        {
            idEnemies.Add(enemy.Id, enemy);
        }
    }

    public CloseEnemy Create(string id)
    {
        if (!idEnemies.TryGetValue(id, out CloseEnemy enemy))
        {
            return null;
        }
        return Instantiate(enemy);
    }
}
