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
    private BulletPoolManager bulletPool;

    protected Vector3 direction;

    public GameObject Prefab { get { return prefab; } }
    public Color Color { get { return color; } }
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Size { get { return size; } }
    public AnimationClip AnimationClip { get { return animationClip; } }


    public override void Behaviour(GameObject caster)
    {
        bulletPool = FindFirstObjectByType<BulletPoolManager>();

        Bullet spawnedSpellBullet = bulletPool.GetBullet();
        spawnedSpellBullet.transform.position = caster.transform.position;
        spawnedSpellBullet.GetComponent<Animation>().clip = animationClip;
        spawnedSpellBullet.GetComponent<BulletSpellControllerClass>().spellData = this;
        

    }
}