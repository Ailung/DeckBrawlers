using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private List<Image> InputImageList;
    [SerializeField] private Sprite InputKick;
    [SerializeField] private Sprite InputPunch;
    [SerializeField] private Color ColorNull;
    [SerializeField] private Color ColorImage;

    private void Awake()
    {
        characterController = FindObjectOfType<CharacterController>();
    }

    private void Update()
    {
        if (characterController != null) 
        {
            for (int i = 0; i < InputImageList.Count; i++)
            {
                //Debug.Log("estoy en el loop con i=" + i + " y el comboList.Count es = "+ characterController.ComboList.Count);
                if (characterController.ComboList.Count > i)
                {
                    if (characterController.ComboList[i] == "kick")
                    {
                        InputImageList[i].sprite = InputKick;
                        InputImageList[i].color = ColorImage;
                    }
                    else if (characterController.ComboList[i] == "punch")
                    {
                        InputImageList[i].sprite = InputPunch;
                        InputImageList[i].color = ColorImage;
                    } else
                    {
                        InputImageList[i].sprite = null;
                        InputImageList[i].color = ColorNull;
                    }
                }else
                {
                    InputImageList[i].sprite = null;
                    InputImageList[i].color = ColorNull;
                }
            }
        }
    }
}
