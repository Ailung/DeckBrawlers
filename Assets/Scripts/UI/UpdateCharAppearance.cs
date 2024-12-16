using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCharappearance : MonoBehaviour
{
    [SerializeField] private GameObject Arsenalface;
    [SerializeField] private GameObject Arsenalhat;
    [SerializeField] private GameObject ArsenalupperBody;
    [SerializeField] private GameObject Arsenalhipps;
    [SerializeField] private GameObject ArsenalLFoot;
    [SerializeField] private GameObject ArsenalRFoot;
    [SerializeField] private GameObject ArsenalLHand;
    [SerializeField] private GameObject ArsenalRHand;
    [SerializeField] private GameObject Appearanceface;
    [SerializeField] private GameObject Appearancehat;
    [SerializeField] private GameObject AppearanceupperBody;
    [SerializeField] private GameObject Appearancehipps;
    [SerializeField] private GameObject AppearanceLFoot;
    [SerializeField] private GameObject AppearanceRFoot;
    [SerializeField] private GameObject AppearanceLHand;
    [SerializeField] private GameObject AppearanceRHand;

    public void UpdateSprite()
    {
        Arsenalface.GetComponent<SpriteRenderer>().sprite = Appearanceface.GetComponent<SpriteRenderer>().sprite;
        Arsenalhat.GetComponent<SpriteRenderer>().sprite = Appearancehat.GetComponent<SpriteRenderer>().sprite;
        ArsenalupperBody.GetComponent<SpriteRenderer>().sprite = AppearanceupperBody.GetComponent<SpriteRenderer>().sprite;
        Arsenalhipps.GetComponent<SpriteRenderer>().sprite = Appearancehipps.GetComponent<SpriteRenderer>().sprite;
        ArsenalLHand.GetComponent<SpriteRenderer>().sprite = AppearanceLHand.GetComponent<SpriteRenderer>().sprite;
        ArsenalRHand.GetComponent<SpriteRenderer>().sprite = AppearanceRHand.GetComponent<SpriteRenderer>().sprite;
        ArsenalLFoot.GetComponent<SpriteRenderer>().sprite = AppearanceLFoot.GetComponent<SpriteRenderer>().sprite;
        ArsenalRFoot.GetComponent<SpriteRenderer>().sprite = AppearanceRFoot.GetComponent<SpriteRenderer>().sprite;
    }
}

