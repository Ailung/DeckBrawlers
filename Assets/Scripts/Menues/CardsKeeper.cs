using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardsKeeper : MonoBehaviour
{
    public static CardsKeeper Instance;

    [SerializeField] public AppearanceCardScriptableClass hat;
    [SerializeField] public AppearanceCardScriptableClass face;
    [SerializeField] public AppearanceCardScriptableClass top;
    [SerializeField] public AppearanceCardScriptableClass hands;
    [SerializeField] public AppearanceCardScriptableClass bottom;
    [SerializeField] public AppearanceCardScriptableClass shoes;

    [SerializeField] public SpellScriptableCardClass card1;
    [SerializeField] public SpellScriptableCardClass card2;
    [SerializeField] public SpellScriptableCardClass card3;
    [SerializeField] public SpellScriptableCardClass card4;
    [SerializeField] public SpellScriptableCardClass card5;

    [SerializeField] public GameObject hatObject;
    [SerializeField] public GameObject faceObject;
    [SerializeField] public GameObject topObject;
    [SerializeField] public GameObject handsObject;
    [SerializeField] public GameObject bottomObject;
    [SerializeField] public GameObject shoesObject;


    public GameObject selectedSpellCard;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        hatObject.GetComponent<SelectedAppearance>().card = hat;
        hatObject.GetComponent<Image>().sprite = hat.appearanceOnSprite;

        faceObject.GetComponent<SelectedAppearance>().card = face;
        faceObject.GetComponent<Image>().sprite = face.appearanceOnSprite;

        topObject.GetComponent<SelectedAppearance>().card = top;
        topObject.GetComponent<Image>().sprite = top.appearanceOnSprite;

        handsObject.GetComponent<SelectedAppearance>().card = hands;
        handsObject.GetComponent<Image>().sprite = hands.appearanceOnSprite;

        bottomObject.GetComponent<SelectedAppearance>().card = bottom;
        bottomObject.GetComponent<Image>().sprite = bottom.appearanceOnSprite;

        shoesObject.GetComponent<SelectedAppearance>().card = shoes;
        shoesObject.GetComponent<Image>().sprite = shoes.appearanceOnSprite;
    }

    public void UpdateAppearance()
    {
        hat = hatObject.GetComponent<SelectedAppearance>().card;
        face = faceObject.GetComponent<SelectedAppearance>().card;
        top = topObject.GetComponent<SelectedAppearance>().card;
        hands = handsObject.GetComponent<SelectedAppearance>().card;
        bottom = bottomObject.GetComponent<SelectedAppearance>().card;
        shoes = shoesObject.GetComponent<SelectedAppearance>().card;
    }

    public void UpdateDeck()
    {
        return;
    }

    public void NewSelectedSpellCard(GameObject spellBeingChanged)
    {
        selectedSpellCard = spellBeingChanged;
    }
}
