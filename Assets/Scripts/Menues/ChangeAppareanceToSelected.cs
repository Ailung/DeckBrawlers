using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ChangeItemToSelected : MonoBehaviour
{
    [SerializeField] private AppearanceCardScriptableClass holdingItem;
    [SerializeField] private GameObject slotToChange;

    private Button buttonClick;

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = holdingItem.appearanceOnSprite;
    }
    public void ChangeSelectedItem()
    {
        slotToChange.GetComponent<SelectedAppearance>().ChangeSelectedCard(holdingItem);
    }


}
