using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceChanger : MonoBehaviour
{
    private SpriteRenderer m_Renderer;
    private CharacterController m_CharacterController;
    [SerializeField] private AppearanceEnum appearanceType;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_CharacterController = GetComponentInParent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        changeAppearance();
    }

    public void changeAppearance()
    {
        switch (appearanceType)
        {
            case AppearanceEnum.top:
                if (m_CharacterController.AppearanceTop.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceTop.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceTop.appearanceColor;
                }
                break;

            case AppearanceEnum.face:
                if (m_CharacterController.AppearanceFace.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceFace.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceFace.appearanceColor;
                }
                break;

            case AppearanceEnum.hands:
                if (m_CharacterController.AppearanceHands.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceHands.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceHands.appearanceColor;
                }
                break;

            case AppearanceEnum.hat:
                if (m_CharacterController.AppearanceHat.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceHat.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceHat.appearanceColor;
                }
                break;

            case AppearanceEnum.bottom:
                if (m_CharacterController.AppearanceBottom.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceBottom.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceBottom.appearanceColor;
                }
                break;

            case AppearanceEnum.skin:
                if (m_CharacterController.AppearanceSkin.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceSkin.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceSkin.appearanceColor;
                }
                break;

            case AppearanceEnum.shape:
                if (m_CharacterController.AppearanceShape.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceShape.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceShape.appearanceColor;
                }
                break;

            case AppearanceEnum.shoes:
                if (m_CharacterController.AppearanceShoes.appearanceOnSprite != null)
                {
                    m_Renderer.sprite = m_CharacterController.AppearanceShoes.appearanceOnSprite;
                    m_Renderer.color = m_CharacterController.AppearanceShoes.appearanceColor;
                }
                break;

            default: break;

        }
    }
}
