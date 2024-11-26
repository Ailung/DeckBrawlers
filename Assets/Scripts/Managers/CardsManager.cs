using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public List<SpellScriptableCardClass> deck = new List<SpellScriptableCardClass>();
    public List<SpellScriptableCardClass> discardPile = new List<SpellScriptableCardClass>();

    [SerializeField] private GameObject cardSlot;
    private SpriteRenderer cardSlotSprite;

    private SpellScriptableCardClass selectedCard;
    public SpellScriptableCardClass SelectedCard { get { return selectedCard; } }

    private void Awake()
    {
        cardSlotSprite = cardSlot.GetComponent<SpriteRenderer>();
        DrawCard();
    }

    public void DrawCard()
    {
        selectedCard = deck[Random.Range(0, deck.Count)];
        cardSlotSprite.sprite = selectedCard.CardAppearance;
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
    public void UseCard(string spellColor, GameObject caster)
    {
        if(spellColor == "orange") selectedCard.OrangeSpell.Behaviour(caster);
        else if (spellColor == "blue") selectedCard.BlueSpell.Behaviour(caster);
        else if (spellColor == "green") selectedCard.GreenSpell.Behaviour(caster);

        discardPile.Add(selectedCard);
        DrawCard();
    }

}
