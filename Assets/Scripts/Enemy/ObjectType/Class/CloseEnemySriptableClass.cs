using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCloseEnemyData", menuName = "ObjectType/Enemy/CloseEnemy", order = 1)]
public class CloseEnemySriptableClass : ScriptableObject
{
    [SerializeField] public int speed = 3;
    [SerializeField] public int contactDamage = 10;
    [SerializeField] public float chaseDistance;
    [SerializeField] public float stopDistance;
}
