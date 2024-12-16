using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemySriptableClass[] Enemies;
    [SerializeField] private AppearanceListForEnemiesClass[] Datas;
    private Dictionary<string, EnemySriptableClass> idEnemies;
    private Dictionary<string, AppearanceListForEnemiesClass> idDatas;
    [SerializeField] private GameObject enemyPrefab;

    private void Awake()
    {
        idEnemies = new Dictionary<string, EnemySriptableClass>();

        foreach (var enemy in Enemies)
        {
            idEnemies.Add(enemy.id, enemy);
        }

        idDatas = new Dictionary<string, AppearanceListForEnemiesClass>();

        foreach (var data in Datas)
        {
            idDatas.Add(data.id, data);
        }
    }

    public GameObject Create(string id, Vector2 pos, string idData)
    {
        if (!idEnemies.TryGetValue(id, out EnemySriptableClass enemyType))
        {
            return null;
        }
        if (!idDatas.TryGetValue(idData, out AppearanceListForEnemiesClass enemyData))
        {
            return null;
        }

        enemyPrefab.GetComponent<EnemyController>().changeEnemyData(enemyType);
        enemyPrefab.GetComponent<EnemyController>().setAppearanceList(enemyData);
        enemyPrefab.transform.position = pos;
        return Instantiate(enemyPrefab);
    }
}
