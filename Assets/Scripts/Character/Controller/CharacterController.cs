using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject foot;
    private bool isFacingRight = true;
    private StateMachine playerStateMachine;

    private float horizontal;
    private float vertical;

    public StateMachine StateMachine => playerStateMachine;

    private Rigidbody2D rb;

    public Rigidbody2D Rb => rb;

    private MeshRenderer mr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        playerStateMachine = new StateMachine(this.gameObject);
        playerStateMachine.Initialize(playerStateMachine.idleState);
        
    }

    void Update()
    {
        playerStateMachine.UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon"))
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
        if (collision.CompareTag("Drop"))
        {

            if (collision.TryGetComponent<Drops>(out Drops drop))
            {
                drop.PickUp();
            }

        }
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputHorizontal < 0 && isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x *-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = false;
        } else if (inputHorizontal > 0 && !isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = true;
        }

        rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
    }
}
