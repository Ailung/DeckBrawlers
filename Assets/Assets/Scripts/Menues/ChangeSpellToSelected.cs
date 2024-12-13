using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpellToSelected : MonoBehaviour
{
    [SerializeField] private SpellScriptableCardClass holdingCard;

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = holdingCard.CardAppearance;
    }
    public void ChangeSelectedItem()
    {
        CardsKeeper.Instance.selectedSpellCard.GetComponent<SelectedSpell>().card = holdingCard;
        CardsKeeper.Instance.selectedSpellCard.GetComponent<Image>().sprite = holdingCard.CardAppearance;
    }
}
