using System.Collections;
using System.Collections.Generic;
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
    private AppearanceCardScriptableClass appearanceHat;
    private AppearanceCardScriptableClass appearanceSkin;
    private AppearanceCardScriptableClass appearanceFace;
    private AppearanceCardScriptableClass appearanceShape;
    private AppearanceCardScriptableClass appearanceTop;
    private AppearanceCardScriptableClass appearanceBottom;
    private AppearanceCardScriptableClass appearanceHands;
    private AppearanceCardScriptableClass appearanceShoes;

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

    }

    void Update()
    {
        playerStateMachine.UpdateState();
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
}
