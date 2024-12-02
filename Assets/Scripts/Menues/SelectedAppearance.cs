using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAppearance : MonoBehaviour
{
    [SerializeField] private AppearanceCardScriptableClass card;

    public void ChangeSelectedCard(AppearanceCardScriptableClass newCard)
    {
        card = newCard;
        this.gameObject.GetComponent<Image>().sprite = newCard.appearanceOnSprite;
    }
}
