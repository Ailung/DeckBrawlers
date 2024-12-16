using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChangeItemToSelected : MonoBehaviour
{
    [SerializeField] private AppearanceCardScriptableClass holdingItem;
    [SerializeField] private GameObject cardToChange;
    [SerializeField] private GameObject spriteToChange;
    [SerializeField] private Boolean isHandsOrFoot;
    [SerializeField] private GameObject spriteToChange2;


    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = holdingItem.appearanceOnSprite;
    }
    public void ChangeSelectedItem()
    {
        cardToChange.GetComponent<SelectedAppearance>().card = holdingItem;
        cardToChange.GetComponent<Image>().sprite = holdingItem.appearanceOnSprite;
        if (!isHandsOrFoot)
        {
            spriteToChange.GetComponent<SpriteRenderer>().sprite = holdingItem.appearanceOnSprite;

        }
        else
        {
            spriteToChange.GetComponent<SpriteRenderer>().sprite = holdingItem.appearanceOnSprite;
            spriteToChange2.GetComponent<SpriteRenderer>().sprite = holdingItem.appearanceOnSprite; 
        }
    }


}
