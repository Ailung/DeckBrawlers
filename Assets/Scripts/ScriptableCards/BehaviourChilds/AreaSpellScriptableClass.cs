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
    [SerializeField] public AnimationClip animationClip;

    public GameObject Prefab { get { return prefab; } }
    public Color Color { get { return color; } }
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Size { get { return size; } }
    public AnimationClip AnimationClip { get { return animationClip; } }


    protected Vector3 direction;

    public override void Behaviour(GameObject caster) {
        GameObject spawnedAreaSpell = Instantiate(prefab);
        spawnedAreaSpell.transform.position = caster.transform.position;
        spawnedAreaSpell.transform.parent = caster.transform;
        spawnedAreaSpell.GetComponent<Animation>().clip = animationClip;
        spawnedAreaSpell.GetComponent<AreaSpellControllerClass>().spellData = this;
    }
}
