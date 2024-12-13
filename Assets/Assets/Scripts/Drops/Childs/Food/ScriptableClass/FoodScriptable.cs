using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFoodData", menuName = "Flyweight/FoodData", order = 1)]

public class FoodScriptable : ScriptableObject
{
    [SerializeField] public Sprite foodSprite;
    [SerializeField] public Animation foodAnimation;
    [SerializeField] public AudioClip foodAudioClip;
    [SerializeField] public int foodAmount;
    [SerializeField] public FoodEnum foodType;
}
