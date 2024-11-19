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
    private int statHealth = 0;
    private int statDefense = 0;
    private int statSpeed = 0;
    private int statAttack = 0;
    private int statAgility = 0;
    [SerializeField] private AppearanceCardScriptableClass[] appearanceCards;
    private List<string> comboList;
    private string comboListTest;
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
    private StateMachine playerStateMachine;

    private float horizontal;
    private float vertical;

    public StateMachine StateMachine => playerStateMachine;
    public int StatAgility => statAgility;

    private Rigidbody2D rb;
    private HealthManager healthManager;

    public Rigidbody2D Rb => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStateMachine = new StateMachine(this.gameObject);
        healthManager = this.GetComponent<HealthManager>();
        playerStateMachine.Initialize(playerStateMachine.idleState);
        appearanceCards = CardManager.Instance.GetAppearanceCards();
        GameManager.Instance.SetCharacter(this);
        

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
            } else if (comboList.SequenceEqual(combo2)) 
            {
                Debug.Log("combo 2 lanzado");
                comboList.Clear();
                comboTimer = 0;
            } else if (comboList.SequenceEqual(combo3))
            {
                Debug.Log("combo 3 lanzado");
                comboList.Clear();
                comboTimer = 0;
            } else
            {
                Debug.Log("ningun lanzado");
                comboList.Clear();
                comboTimer = 0;
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
        comboListTest = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyWeapon"))
        {

            if (collision.TryGetComponent<Weapon>(out Weapon weapon))
            {
                healthManager.getDamage(weapon.AttackDamage - weapon.AttackDamage * (statDefense / 10));
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

        rb.velocity = new Vector2((inputHorizontal * baseSpeed * (statSpeed / 10)) + (inputHorizontal * baseSpeed), (inputVertical * baseSpeed * (statSpeed / 10)) + (inputVertical * baseSpeed));
    }

    public void ResetComboTimer()
    {
        comboTimer = 0;
    }
}
