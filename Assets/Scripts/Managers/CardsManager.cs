using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public List<SpellScriptableCardClass> deck = new List<SpellScriptableCardClass>();
    public List<SpellScriptableCardClass> discardPile = new List<SpellScriptableCardClass>();

    [SerializeField] private Transform cardSlot;
    private bool cardSlotIsAvailable;

    private SpellScriptableCardClass selectedCard;

    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            selectedCard = deck[Random.Range(0, deck.Count)];
        
           if (cardSlotIsAvailable == true)
           {
                selectedCard.CardAppearance.SetActive(true);
                selectedCard.CardAppearance.transform.position = cardSlot.position;
                cardSlotIsAvailable = false;
                deck.Remove(selectedCard);

                if (deck.Count <= 0)
                {
                    foreach (SpellScriptableCardClass card in discardPile)
                    {
                        deck.Add(card);
                    }
                    discardPile.Clear();
                    return;
                }
            }
        
        }
    }

}
