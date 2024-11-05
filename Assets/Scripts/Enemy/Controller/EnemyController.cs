using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private float chaseDistance;
    //[SerializeField] private float stopDistance;
    //[SerializeField] private float chasingDistance = 0.5f;
    //[SerializeField] public string dropId;
    //[SerializeField] public bool startChasing;

    [SerializeField] EnemySriptableClass enemyData;
    private float playerDistance;

    private CharacterController player;
    private GameObject hand;
    private GameObject foot;
    private bool isFacingRight = false;
    private StateMachine enemyStateMachine;
    private DropsFactory dropsFactory;

    public StateMachine StateMachine => enemyStateMachine;
    public float ChaseDistance => enemyData.chaseDistance;
    public float StopDistance => enemyData.stopDistance;
    public float ChasingDistance => enemyData.chasingDistance;
    public float PlayerDistance => playerDistance;
    public float Speed => enemyData.speed;
    public string DropId => enemyData.dropId;
    public CharacterController CharacterController => player;
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    private Rigidbody2D rb;

    public Rigidbody2D Rb => rb;

    private MeshRenderer mr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        enemyStateMachine = new StateMachine(this.gameObject);
        player = FindAnyObjectByType(typeof(CharacterController)).GetComponent<CharacterController>();
        dropsFactory = FindAnyObjectByType<DropsFactory>().GetComponent<DropsFactory>();
        //hand = GetComponentInChildren<Hands>().gameObject;
        //foot = GetComponentInChildren<Leg>().gameObject;
        if (enemyData.startChasing){
            enemyStateMachine.Initialize(enemyStateMachine.chasingState);
        } else
        {
            enemyStateMachine.Initialize(enemyStateMachine.waitingState);
        }
        

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CharacterWeapon"))
        {

            if (collision.TryGetComponent<Weapon>(out Weapon hands))
            {
                this.GetComponent<HealthManager>().getDamage(hands.AttackDamage);
            }

        }
    }

    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        StateMachine.UpdateState();
    }

    public void changeStopAndChaseDistance(float stopDistance, float chaseDistance)
    {
        enemyData.chaseDistance = chaseDistance;
        enemyData.stopDistance = stopDistance;
    }

    public void OnDie()
    {
        Drops drop = dropsFactory.Create(enemyData.dropId);
        if (drop != null)
        {
            drop.gameObject.transform.position = transform.position;
        }
    }

    public void changeEnemyData(EnemySriptableClass data)
    {
        enemyData = data;
    }
}
