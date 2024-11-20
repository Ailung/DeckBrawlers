using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceCardManager : MonoBehaviour
{
    public static AppearanceCardManager Instance;
    [SerializeField] private AppearanceCardScriptableClass[] appearanceCardScriptableClasses;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

    public AppearanceCardScriptableClass[] GetAppearanceCards()
    {
        return appearanceCardScriptableClasses;
    }
}
