using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewBulletSpellData", menuName = "TypeObject/Spells/BulletSpellData", order = 1)]
public class BulletSpellScriptableClass : SpellScriptableBehaviourClass
{
    [SerializeField] GameObject prefab;
    [SerializeField] Color color;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float size;
    [SerializeField] AnimationClip animationClip;

    protected Vector3 direction;

    public override void Behaviour(GameObject caster)
    {
        GameObject spawnedBullet = Instantiate(prefab);
        spawnedBullet.transform.position = caster.transform.position;
    }
}
