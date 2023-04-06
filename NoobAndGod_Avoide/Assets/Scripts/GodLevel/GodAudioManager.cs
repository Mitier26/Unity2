using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAudioManager : MonoBehaviour
{
    public static GodAudioManager Instance;
    public enum SFX { Open, Down, Shock, Impact, Btn, Jump, Death, Pop }
    public AudioClip[] clips;
    public int maxSoundEffects = 5;

    private AudioSource[] audioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        audioSources = new AudioSource[maxSoundEffects];
        for (int i = 0; i < maxSoundEffects; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].volume = 0.5f;
            audioSources[i].playOnAwake = false;
        }
    }

    public void PlaySoundEffect(SFX sfx)
    {
        
        // 사용 가능한 오디오 소스를 찾아서 재생
        for (int i = 0; i < maxSoundEffects; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                if (sfx == SFX.Down) audioSources[i].pitch = 0.3f;
                else audioSources[i].pitch = 1;
                
                audioSources[i].clip = clips[(int)sfx];
                audioSources[i].Play();
                break;
            }
        }
    }
}
