using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Flyweight/WeaponData", order = 1)]

public class WeaponDropScriptable : ScriptableObject
{
    [SerializeField] public GameObject weaponPrefab;
}
