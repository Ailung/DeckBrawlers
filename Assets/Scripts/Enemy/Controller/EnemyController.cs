using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private float chaseDistance;
    //[SerializeField] private float stopDistance;
    //[SerializeField] private float chasingDistance = 0.5f;
    //[SerializeField] public string dropId;
    //[SerializeField] public bool startChasing;

    [SerializeField] EnemySriptableClass enemyData;
    [SerializeField] private AppearanceCardScriptableClass[] appearanceCards;
    private float playerDistance;
    private Random random = new Random();

    private CharacterController player;
    private GameObject hand;
    private GameObject foot;
    private bool isFacingRight = false;
    private bool isStunned = false;
    private StateMachine enemyStateMachine;
    private DropsFactory dropsFactory;

    private AppearanceCardScriptableClass appearanceHat = null;
    private AppearanceCardScriptableClass appearanceSkin = null;
    private AppearanceCardScriptableClass appearanceFace = null;
    private AppearanceCardScriptableClass appearanceShape = null;
    private AppearanceCardScriptableClass appearanceTop = null;
    private AppearanceCardScriptableClass appearanceBottom = null;
    private AppearanceCardScriptableClass appearanceHands = null;
    private AppearanceCardScriptableClass appearanceShoes = null;

    public AppearanceCardScriptableClass AppearanceHat => appearanceHat;
    public AppearanceCardScriptableClass AppearanceSkin => appearanceSkin;
    public AppearanceCardScriptableClass AppearanceFace => appearanceFace;
    public AppearanceCardScriptableClass AppearanceShape => appearanceShape;
    public AppearanceCardScriptableClass AppearanceTop => appearanceTop;
    public AppearanceCardScriptableClass AppearanceBottom => appearanceBottom;
    public AppearanceCardScriptableClass AppearanceHands => appearanceHands;
    public AppearanceCardScriptableClass AppearanceShoes => appearanceShoes;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        enemyStateMachine = new StateMachine(this.gameObject);
        player = FindAnyObjectByType(typeof(CharacterController)).GetComponent<CharacterController>();
        dropsFactory = FindAnyObjectByType<DropsFactory>().GetComponent<DropsFactory>();
        //hand = GetComponentInChildren<Hands>().gameObject;
        //foot = GetComponentInChildren<Leg>().gameObject;
        switch (enemyData.enemyStateEnum) 
        {
            case EnemyStateEnum.chasing:
                enemyStateMachine.Initialize(enemyStateMachine.chasingState);
                break;

            case EnemyStateEnum.waiting:
                enemyStateMachine.Initialize(enemyStateMachine.waitingState);
                break;

            case EnemyStateEnum.casting:
                enemyStateMachine.Initialize(enemyStateMachine.castingState);
                InvokeRepeating("castAttack", 5f, 5f);
                break;

            default:
                enemyStateMachine.Initialize(enemyStateMachine.waitingState);
                break;
        }

        foreach (AppearanceCardScriptableClass card in appearanceCards)
        {
            switch (card.appearanceType)
            {
                case AppearanceEnum.top:
                    appearanceTop = card;
                    break;

                case AppearanceEnum.face:
                    appearanceFace = card;
                    break;

                case AppearanceEnum.hands:
                    appearanceHands = card;
                    break;

                case AppearanceEnum.hat:
                    appearanceHat = card;
                    break;

                case AppearanceEnum.bottom:
                    appearanceBottom = card;
                    break;

                case AppearanceEnum.skin:
                    appearanceSkin = card;
                    break;

                case AppearanceEnum.shape:
                    appearanceShape = card;
                    break;

                case AppearanceEnum.shoes:
                    appearanceShoes = card;
                    break;

                default: break;

            }

        }

        AppearanceChanger[] appearanceChangers = GetComponentsInChildren<AppearanceChanger>();

        Debug.Log(appearanceChangers.Length);


        foreach (AppearanceChanger appearanceChanger in appearanceChangers)
        {
            appearanceChanger.changeAppearance();
        }


    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CharacterWeapon"))
        {

            if (collision.TryGetComponent<Weapon>(out Weapon weapon))
            {
                this.GetComponent<HealthManager>().getDamage(weapon.AttackDamage);
                weapon.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                DamageStun();
                
            }

        }
    }

    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (!isStunned)
        {
            StateMachine.UpdateState();
        }

        

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

    public void setAppearanceList(AppearanceListForEnemiesClass data)
    {
        appearanceCards = data.appearanceCards;
        

        
    }

    private void castAttack()
    {
        int spell = random.Next(1, 3);
        if (enemyStateMachine.CurrentState is Casting)
        {
            if (enemyData.selectedCard != null && enemyData.selectedCard.OrangeSpell != null && spell <= 1)
            {
                Debug.Log("combo 1 lanzado");
                enemyData.selectedCard.OrangeSpell.Behaviour(this.gameObject);
            }
            else if (enemyData.selectedCard != null && enemyData.selectedCard.GreenSpell != null && spell > 1 && spell <= 2)
            {
                Debug.Log("combo 2 lanzado");
                enemyData.selectedCard.GreenSpell.Behaviour(this.gameObject);
            }
            else if (enemyData.selectedCard != null && enemyData.selectedCard.BlueSpell != null && spell > 2 && spell <= 3)
            {
                Debug.Log("combo 3 lanzado");
                enemyData.selectedCard.BlueSpell.Behaviour(this.gameObject);
            }
            else
            {
                Debug.Log("ningun lanzado");
            }
        }
    }

    private IEnumerator stunned()
    {
        yield return new WaitForSeconds(1f);
        isStunned = true;
        yield return new WaitForSeconds(1f);
        isStunned = false;
        yield return new WaitForSeconds(1f);
    }

    public void DamageStun()
    {
        rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        StartCoroutine(stunned());
    }
}
