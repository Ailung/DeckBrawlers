using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Splines;
using UnityEngine;

public class CharacterController : MonoBehaviour
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

    [SerializeField] private int baseSpeed = 3;
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject foot;
    [SerializeField] private GameObject shield;
    private int statHealth = 0;
    private int statDefense = 0;
    private int statSpeed = 0;
    private int statAttack = 0;
    private int statAgility = 0;
    [SerializeField] private AppearanceCardScriptableClass[] appearanceCards;
    private List<string> comboList;
    [SerializeField] private List<string> combo1;
    [SerializeField] private List<string> combo2;
    [SerializeField] private List<string> combo3;
    private float comboTimer;
    [SerializeField] private float comboRefresh;
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
    public List<string> ComboList => comboList;

    private bool isFacingRight = true;
    private bool animationShield = false;
    private bool isStunned = false;

    public bool GetIsFacingRight() { return isFacingRight; }

    private StateMachine playerStateMachine;

    private float horizontal;
    private float vertical;

    public StateMachine StateMachine => playerStateMachine;
    public int StatAgility => statAgility;

    private Rigidbody2D rb;
    private HealthManager healthManager;

    public Rigidbody2D Rb => rb;

    private CardsManager cardsManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStateMachine = new StateMachine(this.gameObject);
        healthManager = this.GetComponent<HealthManager>();
        playerStateMachine.Initialize(playerStateMachine.idleState);
        appearanceCards = AppearanceCardManager.Instance.GetAppearanceCards();
        appearanceCards = CardsKeeper.Instance.appearanceCards.ToArray();
        GameManager.Instance.SetCharacter(this);
        shield.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 0.25f);
        cardsManager = FindObjectOfType<CardsManager>();


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

            statHealth += card.appearanceHP;
            statDefense += card.appearanceDEF;
            statAttack += card.appearanceATK;
            statSpeed += card.appearanceSPD;
            statAgility += card.appearanceDEX;
        }

        healthManager.Initialize(baseHealth * (statHealth / 80) + baseHealth);

        comboList = new List<string>();

    }

    void Update()
    {
       
            
              
        if ((CurrentAnimation == 3 | CurrentAnimation == 4) && (CurrentAnimation != 9))
        {
            Animation(0);
        }

        
        playerStateMachine.UpdateState();
        if (comboList.Count == 3 && comboTimer < comboRefresh)
        {
            if (comboList.SequenceEqual(combo1))
            {
                Debug.Log("combo 1 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("orange", this.gameObject);
                if ((CurrentAnimation != 5) && (CurrentAnimation != 9))
                {
                    Animation(5);
                }
            } else if (comboList.SequenceEqual(combo2)) 
            {
                Debug.Log("combo 2 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("blue", this.gameObject);
                if ((CurrentAnimation != 6) && (CurrentAnimation != 9))
                {
                    Animation(6);
                }
            }
            else if (comboList.SequenceEqual(combo3))
            {
                Debug.Log("combo 3 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("green", this.gameObject);
                if ((CurrentAnimation != 7) && (CurrentAnimation != 9))
                {
                    Animation(7);
                }
            }
            else
            {
                Debug.Log("ningun lanzado");
                
            }
        }

        if (comboTimer > comboRefresh)
        {
            comboList.Clear();
            comboTimer = 0;
        } else
        {
            comboTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon"))
        {

            if (playerStateMachine.CurrentState is not Block)
            {
                if (collision.TryGetComponent<Weapon>(out Weapon weapon))
                {
                    DamageStun(weapon.AttackDamage);
                    weapon.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            } else
            {
                if (collision.TryGetComponent<Weapon>(out Weapon weapon))
                {
                    weapon.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine(ChangeColorShield());
                }
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

    private IEnumerator ChangeColorShield()
    {

        yield return new WaitForSeconds(1f);
        shield.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.25f);
        yield return new WaitForSeconds(1f);
        shield.GetComponent<SpriteRenderer>().color = new Color(0,1,1,0.25f);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator stunned()
    {
        if ((CurrentAnimation != 8) && (CurrentAnimation != 9))
        {
            Animation(8);
        }
        yield return new WaitForSeconds(1f);
        isStunned = false;
    }

    private IEnumerator comboReset()
    {
        yield return new WaitForSeconds(1f);
        comboList.Clear();
        comboTimer = 0;
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");


        Debug.Log(playerStateMachine.CurrentState.GetType().Name);

        if (inputHorizontal < 0 && isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = false;
        } else if (inputHorizontal > 0 && !isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = true;
        }

        if (playerStateMachine.CurrentState is not Block && !isStunned)
        {
            rb.velocity = new Vector2((inputHorizontal * baseSpeed * (statSpeed / 10)) + (inputHorizontal * baseSpeed), (inputVertical * baseSpeed * (statSpeed / 10)) + (inputVertical * baseSpeed));
            if (((rb.velocity.x < -0.2f)) | ((0.2f < rb.velocity.x)))
            {
                if ((CurrentAnimation != 1) && (CurrentAnimation != 9))
                {
                    Animation(1);
                }
            }
            else
            {
                if ((CurrentAnimation != 0) && (CurrentAnimation != 9))
                {
                    Animation(0);
                }
            }
        }
        
    }

    public void ResetComboTimer()
    {
        comboTimer = 0;
    }

    public void Shield(bool state)
    {
        shield.gameObject.SetActive(state);
        if ((CurrentAnimation != 2) && (CurrentAnimation != 9) && state)
        {
            Animation(2);
        }
    }

    public void DamageStun(int spellDamage)
    {
        if (playerStateMachine.CurrentState is not Block)
        {
            healthManager.getDamage(spellDamage - spellDamage * (statDefense / 10));
            isStunned = true;
            rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            StartCoroutine(stunned());
        }
        
    }
    public void Animation(int Int_Index)
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

                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[0];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 1:
                AnimationTimer = -1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[1];
                SplineAHandR.Container = SplineCHandR[1];
                SplineAFeetL.Container = SplineCFeetL[1];
                SplineAFeetR.Container = SplineCFeetR[1];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 2:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[2];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 3:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[3];
                SplineAHandR.Container = SplineCHandR[2];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.XAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 4:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[0];
                SplineAHandR.Container = SplineCHandR[0];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[2];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.ZAxis;  
            break;
            case 5:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[4];
                SplineAHandR.Container = SplineCHandR[3];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 6:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[5];
                SplineAHandR.Container = SplineCHandR[4];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 7:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[6];
                SplineAHandR.Container = SplineCHandR[5];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 8:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[0];
                SplineATorso.Container = SplineCTorso[0];
                SplineAHandL.Container = SplineCHandL[7];
                SplineAHandR.Container = SplineCHandR[6];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

                SplineAHead.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineATorso.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAHandR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetL.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
                SplineAFeetR.ObjectUpAxis = UnityEngine.Splines.SplineComponent.AlignAxis.YAxis;
            break;
            case 9:
                AnimationTimer = 1;
                SplineAHead.Container = SplineCHead[1];
                SplineATorso.Container = SplineCTorso[1];
                SplineAHandL.Container = SplineCHandL[8];
                SplineAHandR.Container = SplineCHandR[7];
                SplineAFeetL.Container = SplineCFeetL[0];
                SplineAFeetR.Container = SplineCFeetR[0];

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

