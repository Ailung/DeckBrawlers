using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewBulletSpellData", menuName = "TypeObject/Spells/BulletSpellData", order = 1)]
public class BulletSpellScriptableClass : SpellScriptableBehaviourClass
{
    [SerializeField] GameObject prefab;
    [SerializeField] Color color;
    [SerializeField] int damage;
    [SerializeField] int bulletAnimation;
    [SerializeField] float speed;
    [SerializeField] float size;
    [SerializeField] string tag;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] private string comboDescription;
    private BulletPoolManager bulletPool;

    protected Vector3 direction;

    public GameObject Prefab { get { return prefab; } }
    public Color Color { get { return color; } }
    public int Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Size { get { return size; } }
    public string Tag { get { return tag; } }
    public AnimationClip AnimationClip { get { return animationClip; } }
    public string ComboDescription { get { return comboDescription; } }


    public override void Behaviour(GameObject caster)
    {
        bulletPool = FindFirstObjectByType<BulletPoolManager>();

        Bullet spawnedSpellBullet = bulletPool.GetBullet();
        spawnedSpellBullet.transform.position = caster.transform.position;
        spawnedSpellBullet.GetComponent<Animator>().SetInteger("BulletAnimation",bulletAnimation);
        spawnedSpellBullet.GetComponent<BulletSpellControllerClass>().spellData = this;
        

    }
}
