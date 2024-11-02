using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float stopDistance;
    [SerializeField] private float chasingDistance = 0.5f;
    private float playerDistance;

    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject foot;
    [SerializeField] private CharacterController player;
    private bool isFacingRight = false;
    private StateMachine enemyStateMachine;

    public StateMachine StateMachine => enemyStateMachine;
    public float ChaseDistance => chaseDistance;
    public float StopDistance => stopDistance;
    public float ChasingDistance => chasingDistance;
    public float PlayerDistance => playerDistance;
    public float Speed => speed;
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
        enemyStateMachine.Initialize(enemyStateMachine.waitingState);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CharacterWeapon"))
        {

            if (collision.TryGetComponent<Hands>(out Hands hands))
            {
                this.GetComponent<HealthManager>().getDamage(hands.AttackDamage);
            }
            if (collision.TryGetComponent<Leg>(out Leg leg))
            {
                this.GetComponent<HealthManager>().getDamage(leg.AttackDamage);
            }

        }
    }

    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        StateMachine.UpdateState();
    }

    private void StopChasePlayer()
    {
        
        hand.GetComponent<Hands>().Attack();
        
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x && isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = false;
        }
        else if (transform.position.x > player.transform.position.x && !isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void changeStopAndChaseDistance(float stopDistance, float chaseDistance)
    {
        this.chaseDistance = chaseDistance; 
        this.stopDistance = stopDistance;
    }
}
