using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
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
        playerStateMachine.UpdateState();
        if (comboList.Count == 3 && comboTimer < comboRefresh)
        {
            if (comboList.SequenceEqual(combo1))
            {
                Debug.Log("combo 1 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("orange", this.gameObject);
            } else if (comboList.SequenceEqual(combo2)) 
            {
                Debug.Log("combo 2 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("blue", this.gameObject);
            }
            else if (comboList.SequenceEqual(combo3))
            {
                Debug.Log("combo 3 lanzado");
                comboList.Clear();
                comboTimer = 0;
                cardsManager.UseCard("green", this.gameObject);
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
        }
        
    }

    public void ResetComboTimer()
    {
        comboTimer = 0;
    }

    public void Shield(bool state)
    {
        shield.gameObject.SetActive(state);
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
}
