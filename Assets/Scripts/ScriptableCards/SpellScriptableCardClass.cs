using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewSpellCardData", menuName ="TypeObject/SpellCardData", order = 1) ]
public class SpellScriptableCardClass : ScriptableObject
{
    [SerializeField] private SpellScriptableBehaviourClass orangeSpell;
    [SerializeField] private SpellScriptableBehaviourClass blueSpell;
    [SerializeField] private SpellScriptableBehaviourClass greenSpell;
    [SerializeField] private Sprite cardAppearance;
    [SerializeField] private string orangeComboDescription;
    [SerializeField] private string blueComboDescription;
    [SerializeField] private string greenComboDescription;

    public SpellScriptableBehaviourClass OrangeSpell { get { return orangeSpell; } }
    public SpellScriptableBehaviourClass BlueSpell { get { return blueSpell; } }
    public SpellScriptableBehaviourClass GreenSpell { get { return greenSpell; } }
    public Sprite CardAppearance { get { return cardAppearance; } }
    public string OrangeComboDescription { get { return orangeComboDescription; } }
    public string BlueComboDescription { get { return blueComboDescription; } }
    public string GreenComboDescription { get { return greenComboDescription; } }
}
