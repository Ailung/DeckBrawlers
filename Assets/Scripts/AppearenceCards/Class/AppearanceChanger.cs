using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppearanceChanger : MonoBehaviour
{
    private SpriteRenderer m_Renderer;
    private GameObject m_GameObject;
    private CharacterController m_CharacterController = null;
    private EnemyController m_EnemyController = null;
    [SerializeField] private AppearanceEnum appearanceType;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] public Color defaultAppearanceColor;

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_CharacterController = GetComponentInParent<CharacterController>();
        m_EnemyController = GetComponentInParent<EnemyController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        changeAppearance();
    }

    public void changeAppearance()
    {
        
        if (m_CharacterController != null)
        {
            switch (appearanceType)
            {
                case AppearanceEnum.top:
                    if (!m_CharacterController.AppearanceTop.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceTop.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceTop.appearanceColor;
                    } else 
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.face:
                    if (!m_CharacterController.AppearanceFace.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceFace.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceFace.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.hands:
                    if (!m_CharacterController.AppearanceHands.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceHands.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceHands.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.hat:
                    if (!m_CharacterController.AppearanceHat.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceHat.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceHat.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.bottom:
                    if (!m_CharacterController.AppearanceBottom.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceBottom.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceBottom.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.skin:
                    if (!m_CharacterController.AppearanceSkin.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceSkin.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceSkin.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.shape:
                    if (!m_CharacterController.AppearanceShape.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceShape.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceShape.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.shoes:
                    if (!m_CharacterController.AppearanceShoes.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_CharacterController.AppearanceShoes.appearanceOnSprite;
                        m_Renderer.color = m_CharacterController.AppearanceShoes.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                default: break;

            }
        }

        if (m_EnemyController != null)
        {
            switch (appearanceType)
            {
                case AppearanceEnum.top:
                    if (!m_EnemyController.AppearanceTop.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceTop.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceTop.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.face:
                    if (!m_EnemyController.AppearanceFace.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceFace.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceFace.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.hands:
                    if (!m_EnemyController.AppearanceHands.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceHands.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceHands.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.hat:
                    if (!m_EnemyController.AppearanceHat.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceHat.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceHat.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.bottom:
                    if (!m_EnemyController.AppearanceBottom.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceBottom.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceBottom.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.skin:
                    if (!m_EnemyController.AppearanceSkin.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceSkin.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceSkin.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.shape:
                    if (!m_EnemyController.AppearanceShape.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceShape.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceShape.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                case AppearanceEnum.shoes:
                    if (!m_EnemyController.AppearanceShoes.appearanceOnSprite.IsUnityNull())
                    {
                        m_Renderer.sprite = m_EnemyController.AppearanceShoes.appearanceOnSprite;
                        m_Renderer.color = m_EnemyController.AppearanceShoes.appearanceColor;
                    }
                    else
                    {
                        m_Renderer.sprite = defaultSprite;
                        m_Renderer.color = defaultAppearanceColor;
                    }
                    break;

                default: break;

            }
        }
    }
}
