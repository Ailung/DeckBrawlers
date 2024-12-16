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
    [SerializeField] int ConeAnimation;
    [SerializeField] float speed;
    [SerializeField] float size;
    [SerializeField] string tag;
    [SerializeField] public AnimationClip animationClip;
    [SerializeField] private string comboDescription;

    public GameObject Prefab { get { return prefab; } }
    public Color Color { get { return color; } }
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Size { get { return size; } }
    public string Tag { get { return tag; } }
    public AnimationClip AnimationClip { get { return animationClip; } }
    public string ComboDescription { get { return comboDescription; } }


    protected Vector3 direction;

    public override void Behaviour(GameObject caster)
    {
        prefab.GetComponent<ConeSpellControllerClass>().spellData = this;
        GameObject spawnedConeSpell = Instantiate(prefab);

        spawnedConeSpell.transform.position = caster.transform.position + new Vector3(3,0,0);
        spawnedConeSpell.transform.parent = caster.transform;

        spawnedConeSpell.GetComponent<Animator>().SetInteger("ConeAnimation", ConeAnimation);
        
    }
}
