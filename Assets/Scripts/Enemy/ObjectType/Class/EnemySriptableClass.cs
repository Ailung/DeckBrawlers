using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "ObjectType/Enemy/Enemy", order = 1)]
public class EnemySriptableClass : ScriptableObject
{
    [SerializeField] public string id = "id";
    [SerializeField] public int speed = 3;
    [SerializeField] public float chaseDistance;
    [SerializeField] public float stopDistance;
    [SerializeField] public float chasingDistance = 0.5f;
    [SerializeField] public string dropId;
    [SerializeField] public bool startChasing;
}
