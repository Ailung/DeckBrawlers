using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract int AttackDamage { get; }
    public abstract int AttackSpeed { get; }
    public abstract CharacterController Player { get; }
    public abstract bool IsAttacking { get; }
    public abstract void Attack();
}
