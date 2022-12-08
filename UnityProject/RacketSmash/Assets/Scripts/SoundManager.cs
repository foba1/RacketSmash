using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Slider")]
    [SerializeField] GameObject bgmSllider;
    [SerializeField] GameObject effectSllider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BGM") || !PlayerPrefs.HasKey("Effect"))
        {
            PlayerPrefs.SetFloat("BGM", 1f);
            PlayerPrefs.SetFloat("Effect", 1f);
            PlayerPrefs.Save();
        }
        else
        {
            bgmSllider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGM");
            effectSllider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Effect");
        }
    }

    public void UpdateBGM()
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            PlayerPrefs.SetFloat("BGM", bgmSllider.GetComponent<Slider>().value);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetFloat("BGM", 1f);
            PlayerPrefs.Save();
        }
    }

    public void UpdateEffect()
    {
        if (PlayerPrefs.HasKey("Effect"))
        {
            PlayerPrefs.SetFloat("Effect", effectSllider.GetComponent<Slider>().value);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetFloat("Effect", 1f);
            PlayerPrefs.Save();
        }
    }
}
