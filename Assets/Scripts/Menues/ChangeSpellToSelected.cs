using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpellToSelected : MonoBehaviour
{
    [SerializeField] private SpellScriptableCardClass holdingCard;
    [SerializeField] private GameObject cardToChange;

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = holdingCard.CardAppearance;
    }
    public void ChangeSelectedItem()
    {
        cardToChange.GetComponent<SelectedSpell>().card = holdingCard;
        cardToChange.GetComponent<Image>().sprite = holdingCard.CardAppearance;
    }
}
