using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpellToSelected : MonoBehaviour
{
    [SerializeField] private SpellScriptableCardClass holdingCard;
    [SerializeField] private TextMeshProUGUI comboDescription1;
    [SerializeField] private TextMeshProUGUI comboDescription2;
    [SerializeField] private TextMeshProUGUI comboDescription3;

    private string descripcion1;
    private string descripcion2;
    private string descripcion3;

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = holdingCard.CardAppearance;
        comboDescription1.text = holdingCard.OrangeComboDescription;
        comboDescription2.text = holdingCard.BlueComboDescription;
        comboDescription3.text = holdingCard.GreenComboDescription;
    }
    public void ChangeSelectedItem()
    {
        CardsKeeper.Instance.selectedSpellCard.GetComponent<SelectedSpell>().card = holdingCard;
        CardsKeeper.Instance.selectedSpellCard.GetComponent<Image>().sprite = holdingCard.CardAppearance;
    }
}
