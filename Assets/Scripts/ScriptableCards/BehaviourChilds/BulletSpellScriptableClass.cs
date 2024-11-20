using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletSpellData", menuName = "TypeObject/Spells/BulletSpellData", order = 1)]
public class BulletSpellScriptableClass : SpellScriptableBehaviourClass
{
    [SerializeField] Sprite sprite; 
    [SerializeField] Color color; 
    [SerializeField] int damage; 
    [SerializeField] float speed; 
    [SerializeField] float size;
    [SerializeField] AnimationClip animationClip;

    public override void Behaviour() { }
}
