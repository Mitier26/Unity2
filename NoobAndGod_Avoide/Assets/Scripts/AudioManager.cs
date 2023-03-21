using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public enum SFX { Coin, CountDown, GameOver, Water }
    public AudioClip[] clips;
    public int maxSoundEffects = 5;

    private AudioSource[] audioSources;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSources = new AudioSource[maxSoundEffects];
        for (int i = 0; i < maxSoundEffects; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].volume = 0.1f;
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
                audioSources[i].clip = clips[(int)sfx];
                audioSources[i].Play();
                break;
            }
        }
    }
}