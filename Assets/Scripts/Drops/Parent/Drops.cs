using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drops : MonoBehaviour
{
    public abstract string Id { get; }

    public abstract void PickUp();
}
