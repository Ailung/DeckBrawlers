using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCoinData", menuName = "Flyweight/CoinData", order = 1)]

public class CoinScriptable : ScriptableObject
{
    [SerializeField] public Sprite coinSprite;
    [SerializeField] public Animation coinAnimation;
    [SerializeField] public AudioClip coinAudioClip;
    [SerializeField] public int coinAmount;
    [SerializeField] public CoinEnum coinType;
    
}
