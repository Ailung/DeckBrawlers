using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeEnemyData", menuName = "ObjectType/Enemy/RangeEnemy", order = 1)]
public class RangeEnemyScriptableClass : ScriptableObject
{
    [SerializeField] public int speed = 3;
    [SerializeField] public int contactDamage = 10;
    [SerializeField] public float chaseDistance;
    [SerializeField] public float stopDistance;
    [SerializeField] public float attackSpeed = 0.2f;
    [SerializeField] public int attackDamage = 10;
    [SerializeField] public string dropId;
}
