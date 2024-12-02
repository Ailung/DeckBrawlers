using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    [SerializeField] private string audioSource;

    private void Start()
    {
        if (audioSource == "sfx")
        {
            AudioManager.Instance.setSFXSource(GetComponent<AudioSource>());
        } else
        {
            AudioManager.Instance.setMusicSource(GetComponent<AudioSource>());
        }
        
    }
}
