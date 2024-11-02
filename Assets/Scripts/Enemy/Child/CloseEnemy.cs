using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemy : Enemy
{
    [SerializeField] private string id;
    [SerializeField] public CloseEnemySriptableClass data;
    [SerializeField] public string dropId;
    
    private CharacterController player;
    private float playerDistance;
    private bool isFacingRight = false;
    private StateMachine enemyStateMachine;
    private Rigidbody2D rb;
    private DropsFactory dropsFactory;
    

    public override StateMachine StateMachine => enemyStateMachine;
    public override float ChaseDistance => data.chaseDistance;
    public override float StopDistance => data.stopDistance;
    public override int ContactDamage => data.contactDamage;
    public override float PlayerDistance => data.stopDistance;
    public override bool IsFacingRight => isFacingRight;
    public override string Id => id;


    public override Rigidbody2D Rb => rb;

    private MeshRenderer mr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        player = FindFirstObjectByType<CharacterController>();
        dropsFactory = FindFirstObjectByType<DropsFactory>();
        //enemyStateMachine = new StateMachine(gameObject);
        //enemyStateMachine.Initialize(enemyStateMachine.idleState);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullets"))
        {
            GetComponent<HealthManager>().getDamage(collision.GetComponent<Bullet>().damage);
            collision.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance < data.chaseDistance && playerDistance > data.stopDistance)
        {
            ChasePlayer();
        }
        if (playerDistance <= data.stopDistance)
        {
            StopChasePlayer();
        }
    }

    public override void OnDie()
    {
        Drops drop = dropsFactory.Create(dropId);
        if (drop != null)
        {
            drop.gameObject.transform.position = transform.position;
        }
    }

    public override void StopChasePlayer()
    {
        
        
        
    }

    public override void ChasePlayer()
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
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), data.speed * Time.deltaTime);
    }
}
