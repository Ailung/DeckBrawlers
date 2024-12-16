using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SplineContainer[] SplineCHead;
        // 0) Head Default  
        // 1) Head Death    
    [SerializeField] private SplineAnimate SplineAHead;
    [SerializeField] private SplineContainer[] SplineCTorso;
        // 0) Torso Default 
        // 1) Torso Death   
    [SerializeField] private SplineAnimate SplineATorso;
    [SerializeField] private SplineContainer[] SplineCHandL;
        // 0) HandL Idle 
        // 1) HandL Walk
        // 2) HandL Block
        // 3) HandL Punch 
        // 4) HandL SpellA
        // 5) HandL SpellB
        // 6) HandL SpellC
        // 7) HandL Hurt
        // 8) HandL Death
    [SerializeField] private SplineAnimate SplineAHandL;
    [SerializeField] private SplineContainer[] SplineCHandR;
        // 0) HandR Idle 
        // 1) HandR Walk
        // 2) HandR Punch 
        // 3) HandR SpellA
        // 4) HandR SpellB
        // 5) HandR SpellC
        // 6) HandR Hurt
        // 7) HandR Death
    [SerializeField] private SplineAnimate SplineAHandR;
    [SerializeField] private SplineContainer[] SplineCFeetL;
        // 0) FootL Default
        // 1) FootL Walk
    [SerializeField] private SplineAnimate SplineAFeetL;
    [SerializeField] private SplineContainer[] SplineCFeetR;
        // 0) FootR Default
        // 1) FootR Walk
        // 2) FootR Kick
    [SerializeField] private SplineAnimate SplineAFeetR;
    private float AnimationTimer;
    private float AnimationCounter;
    public int CurrentAnimation;

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
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject foot;
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
        
        if ((CurrentAnimation != 1) & (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
        {
            Animation(1, 1);
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
        if (hand.gameObject.activeSelf)
        {
            if ((CurrentAnimation != 3) & (CurrentAnimation != 9))
            {
                Animation(3,1);
            }
        }
        else
        {
            if (foot.gameObject.activeSelf)
            {
                if ((CurrentAnimation != 4) & (CurrentAnimation != 9))
                {
                    Animation(4,1);
                }
            }
            else
            {          
                if ((CurrentAnimation == 3 | CurrentAnimation == 4) && (CurrentAnimation != 9))
                {
                    Animation(1, 1);
                }
            }
        }
        Debug.Log("E: "+CurrentAnimation);

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
            if (enemyData.selectedCard != null && enemyData.selectedCard.OrangeSpell != null)
            {
                if (enemyData.selectedCard != null && enemyData.selectedCard.GreenSpell != null)
                {
                    if (enemyData.selectedCard != null && enemyData.selectedCard.BlueSpell != null)
                    {
                        if (spell <= 1)
                        {
                            Debug.Log("combo 1 lanzado");
                            if ((CurrentAnimation != 5) && (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
                            {
                                Animation(5, 1);
                            }
                            else
                            {
                                Animation(1, 1);
                            }
                            enemyData.selectedCard.OrangeSpell.Behaviour(this.gameObject);
                        }
                        else if (spell > 1 && spell <= 2)
                        {
                            Debug.Log("combo 2 lanzado");
                            if ((CurrentAnimation != 6) && (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
                            {
                                Animation(6, 1);
                            }
                            else
                            {
                                Animation(1, 1);
                            }
                            enemyData.selectedCard.GreenSpell.Behaviour(this.gameObject);
                        }
                        else if (spell > 2 && spell <= 3)
                        {
                            Debug.Log("combo 3 lanzado");
                            if ((CurrentAnimation != 7) && (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
                            {
                                Animation(7, 1);
                            }
                            else
                            {
                                Animation(1, 1);
                            }
                            enemyData.selectedCard.BlueSpell.Behaviour(this.gameObject);
                        }
                        else
                        {
                            Debug.Log("ningun lanzado");
                            if ((CurrentAnimation != 1) & (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
                            {
                                Animation(1, 1);
                            }
                        }
                    }
                }

                if (enemyData.selectedCard != null && enemyData.selectedCard.GreenSpell == null)
                {
                    if (enemyData.selectedCard != null && enemyData.selectedCard.BlueSpell == null)
                    {
                        Debug.Log("combo 1 lanzado");
                        if ((CurrentAnimation != 5) && (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
                        {
                            Animation(5, 1);
                        }
                            else
                            {
                                Animation(1, 1);
                            }
                        enemyData.selectedCard.OrangeSpell.Behaviour(this.gameObject);
                    }
                }
            }
            
        }
    }

    private IEnumerator stunned()
    {
        if ((CurrentAnimation != 8) && (CurrentAnimation != 9) && !hand.activeSelf && !foot.activeSelf)
        {
            Animation(8, 1);
        }
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
    public void Animation(int Int_Index, float time)
    {
        // 0) Idle 
        // 1) Walk
        // 2) Block
        // 3) Punch 
        // 4) Kick 
        // 5) SpellA
        // 6) SpellB
        // 7) SpellC
        // 8) Hurt
        // 9) Death
        AnimationCounter = 0;
        CurrentAnimation = Int_Index;
        switch (Int_Index)
        {
            case 0:
                AnimationTimer = -1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Loop;
                SplineATorso.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Loop;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[0];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 1:
                AnimationTimer = -1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Loop;
                SplineATorso.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Loop;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[1];
                SplineAHandR.Container = SplineCHandR[1];
                SplineAFeetL.Container = SplineCFeetL[1];
                SplineAFeetR.Container = SplineCFeetR[1];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 2:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Loop;
                SplineATorso.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Loop;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Loop;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[2];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 3:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration =  time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[3];
                SplineAHandR.Container = SplineCHandR[2];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 4:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[0];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[2];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.ZAxis;  
            break;
            case 5:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[4];
                SplineAHandR.Container = SplineCHandR[3];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 6:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[5];
                SplineAHandR.Container = SplineCHandR[4];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 7:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[6];
                SplineAHandR.Container = SplineCHandR[5];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 8:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[7];
                SplineAHandR.Container = SplineCHandR[6];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 9:
                AnimationTimer = 1;

                SplineAHead.Loop = SplineAnimate.LoopMode.Once;
                SplineATorso.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandL.Loop = SplineAnimate.LoopMode.Once;
                SplineAHandR.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetL.Loop = SplineAnimate.LoopMode.Once;
                SplineAFeetR.Loop = SplineAnimate.LoopMode.Once;

                SplineAHead.Duration = time;
                SplineATorso.Duration = time;
                SplineAHandL.Duration = time;
                SplineAHandR.Duration = time;
                SplineAFeetL.Duration = time;
                SplineAFeetR.Duration = time;

                SplineAHead.Container = SplineCHead[1];
                SplineATorso.Container = SplineCTorso[1];
                SplineAHandL.Container = SplineCHandL[8];
                SplineAHandR.Container = SplineCHandR[7];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.Restart(true);
                SplineATorso.Restart(true);
                SplineAHandL.Restart(true);
                SplineAHandR.Restart(true);
                SplineAFeetL.Restart(true);
                SplineAFeetR.Restart(true);

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;

            break;
        }
    }
}

