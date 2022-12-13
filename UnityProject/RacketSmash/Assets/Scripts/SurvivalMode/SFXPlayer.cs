using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalMode
{
    [System.Serializable]
    public class SoundData
    {
        public string key;
        public AudioClip clip;
    }
    public class SFXPlayer : MonoBehaviour
    {
        AudioSource audioSource;
        [SerializeField] List<SoundData> sounds;


        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void PlaySound(string key)
        {
            List<SoundData> matches = sounds.FindAll(x => x.key == key);
            foreach(SoundData sound in matches)
                audioSource.PlayOneShot(sound.clip);
        }
    }

}