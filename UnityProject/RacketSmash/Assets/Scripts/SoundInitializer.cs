using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInitializer : MonoBehaviour
{
    [Header("IsBGM")]
    [SerializeField] bool isBGM;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            if (isBGM)
            {
                if (PlayerPrefs.HasKey("BGM"))
                {
                    audioSource.volume = PlayerPrefs.GetFloat("BGM");
                }
                else
                {
                    audioSource.volume = 1f;
                    PlayerPrefs.SetFloat("BGM", 1f);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                if (PlayerPrefs.HasKey("Effect"))
                {
                    audioSource.volume = PlayerPrefs.GetFloat("Effect");
                }
                else
                {
                    audioSource.volume = 1f;
                    PlayerPrefs.SetFloat("Effect", 1f);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
