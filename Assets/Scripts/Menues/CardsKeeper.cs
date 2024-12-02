using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


}
