using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CardTimer : MonoBehaviour
{
    [SerializeField] private float cardTime = 0f;
    private float targetTime = 0f;
    public CardsManager cardsManager;

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f )
        {
            timerEnded();
        }
    }
    private void Start()
    {
        targetTime = cardTime;
    }

    private void timerEnded()
    {
        cardsManager.DrawCard();
        targetTime = cardTime;

    }
}
