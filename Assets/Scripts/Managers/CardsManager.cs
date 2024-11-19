using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public Text deckSizeText;
    public Text discardSizeText;

    private Card randCard;

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            randCard = deck[Random.Range(0, deck.Count)];
            for (int i = 0; i < cardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasBeenPlayed = false;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardSizeText.text = discardPile.Count.ToString();
        if (deck.Count <= 0)
        {
            foreach (Card card in discardPile) 
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }
}
