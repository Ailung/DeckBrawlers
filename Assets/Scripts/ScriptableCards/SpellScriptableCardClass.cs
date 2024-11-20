using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewSpellCardData", menuName ="TypeObject/SpellCardData", order = 1) ]
public class SpellScriptableCardClass : ScriptableObject
{
    [SerializeField] public SpellScriptableBehaviourClass orangeSpell;
    [SerializeField] public SpellScriptableBehaviourClass blueSpell;
    [SerializeField] public SpellScriptableBehaviourClass greenSpell;
}
