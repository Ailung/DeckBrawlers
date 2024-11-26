using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "NewConeSpellData", menuName = "TypeObject/Spells/ConeSpellData", order = 1)]
public class ConeSpellScriptableClass : SpellScriptableBehaviourClass
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

    public override void Behaviour(GameObject caster)
    {
        GameObject spawnedConeSpell = Instantiate(prefab);

        spawnedConeSpell.transform.position = caster.transform.position + new Vector3(3,0,0);
        spawnedConeSpell.transform.parent = caster.transform;

        spawnedConeSpell.GetComponent<Animation>().clip = animationClip;
        spawnedConeSpell.GetComponent<ConeSpellControllerClass>().spellData = this;
    }
}
