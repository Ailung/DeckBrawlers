using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract StateMachine StateMachine { get; }
    public abstract float ChaseDistance { get; }
    public abstract float StopDistance { get; }
    public abstract float PlayerDistance { get; }
    public abstract int ContactDamage { get; }
    public abstract bool IsFacingRight { get; }
    public abstract string Id { get; }
    public abstract Rigidbody2D Rb { get; }

    public abstract void StopChasePlayer();

    public abstract void ChasePlayer();

    public abstract void OnDie();
}
