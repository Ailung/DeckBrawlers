using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;

    public CardsManager cm;

    CharacterController characterController;
    CardBullet cardBulletScript;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        cm = FindObjectOfType<CardsManager>();
        characterController = FindObjectOfType<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { PlayCard1(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { PlayCard2(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { PlayCard3(); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { PlayCard4(); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { PlayCard5(); }
    }
    //private void OnMouseDown()
    //{
    //    if (hasBeenPlayed == false) {
    //        Debug.Log(handIndex);
    //        transform.position += Vector3.down * -5;
    //        hasBeenPlayed = true;
    //        cm.availableCardSlots[handIndex] = true;
    //        Invoke("MoveToDiscardPile", 1f);

    //    }
    //}
    public void PlayCard1()
    {
        if(handIndex == 0 && hasBeenPlayed == false)
        {
            transform.position += Vector3.down * 15;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            ShootCard();
            Invoke("MoveToDiscardPile", 2f);

        }
    }
    public void PlayCard2()
    {
        if (handIndex == 1 && hasBeenPlayed == false)
        {
            transform.position += Vector3.down * 15;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            ShootCard();
            Invoke("MoveToDiscardPile", 2f);
        }
    }
    public void PlayCard3()
    {
        if (handIndex == 2 && hasBeenPlayed == false)
        {
            transform.position += Vector3.down * 15;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            ShootCard();
            Invoke("MoveToDiscardPile", 2f);
        }
    }
    public void PlayCard4()
    {
        if (handIndex == 3 && hasBeenPlayed == false)
        {
            transform.position += Vector3.down * 15;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            ShootCard();
            Invoke("MoveToDiscardPile", 2f);
        }
    }
    public void PlayCard5()
    {
        if (handIndex == 4 && hasBeenPlayed == false)
        {
            transform.position += Vector3.down * 15;
            hasBeenPlayed = true;
            cm.availableCardSlots[handIndex] = true;
            ShootCard();
            Invoke("MoveToDiscardPile", 2f);
        }
    }

    void MoveToDiscardPile() 
    { 
        cm.discardPile.Add(this);
        gameObject.SetActive(false);

    }
    private void ShootCard()
    {
        GameObject bullet = CardAttackPool.instance.GetPooledCardAttack();
        if (bullet != null)
        {
            bullet.transform.position = characterController.transform.position;

            bullet.SetActive(true);
            CardBullet cardBullet = bullet.GetComponent<CardBullet>();
            bool facingRight = characterController.GetIsFacingRight();
            Color currentColor = spriteRenderer.color;
            cardBullet.ShootRight(facingRight,currentColor);

        }

    }
}
