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
    [SerializeField] int AreaAnimation; 
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
        prefab.GetComponent<Animation>().clip = animationClip;
        prefab.GetComponent<AreaSpellControllerClass>().spellData = this;
        GameObject spawnedAreaSpell = Instantiate(prefab);

        spawnedAreaSpell.transform.position = caster.transform.position;
        spawnedAreaSpell.transform.parent = caster.transform;

        spawnedAreaSpell.GetComponent<Animator>().SetInteger("AreaAnimation", AreaAnimation);
        
        

    }
}
