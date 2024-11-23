using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "NewAreaSpellData", menuName = "TypeObject/Spells/AreaSpellData", order = 1)]
public class AreaSpellScriptableClass : SpellScriptableBehaviourClass
{
    [SerializeField] GameObject prefab; 
    [SerializeField] Color color; 
    [SerializeField] int damage; 
    [SerializeField] float speed; 
    [SerializeField] float size;
    [SerializeField] AnimationClip animationClip;

    protected Vector3 direction;

    public override void Behaviour(GameObject caster) {
        GameObject spawnedGarlic = Instantiate(prefab);
        spawnedGarlic.transform.position = caster.transform.position;
        spawnedGarlic.transform.parent = caster.transform;
    }
}
