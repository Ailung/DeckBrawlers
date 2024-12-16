using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAppearanceListForEnemiesData", menuName = "Flyweight/AppearanceListForEnemiesData", order = 1)]
public class AppearanceListForEnemiesClass : ScriptableObject
{
    [SerializeField] public string id;
    [SerializeField] public AppearanceCardScriptableClass[] appearanceCards;
}
