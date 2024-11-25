using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewSpellCardData", menuName ="TypeObject/SpellCardData", order = 1) ]
public class SpellScriptableCardClass : ScriptableObject
{
    [SerializeField] private SpellScriptableBehaviourClass orangeSpell;
    [SerializeField] private SpellScriptableBehaviourClass blueSpell;
    [SerializeField] private SpellScriptableBehaviourClass greenSpell;
    [SerializeField] private GameObject cardAppearance;

    public SpellScriptableBehaviourClass OrangeSpell { get { return orangeSpell; } }
    public SpellScriptableBehaviourClass BlueSpell { get { return blueSpell; } }
    public SpellScriptableBehaviourClass GreenSpell { get { return greenSpell; } }
    public GameObject CardAppearance { get { return cardAppearance; } }
}
