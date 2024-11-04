using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAppearanceCardData", menuName = "Flyweight/AppearanceCardData", order = 1)]

public class AppearanceCardScriptableClass : ScriptableObject
{
    [SerializeField] public Sprite appearanceOnSprite;
    [SerializeField] public Sprite appearanceOffSprite;
    [SerializeField] public Animation appearanceAnimation;
    [SerializeField] public AudioClip appearanceAudioClip;
    [SerializeField] public int appearanceHP;
    [SerializeField] public int appearanceDEF;
    [SerializeField] public int appearanceATK;
    [SerializeField] public int appearanceDEX;
    [SerializeField] public int appearanceSPD;
    [SerializeField] public AppearanceEnum appearanceType;
}
