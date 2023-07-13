using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobAudioManager : MonoBehaviour
{
    public static NoobAudioManager instance;

    [Header("BGM")]
    AudioSource bgmSource;
    public AudioClip bgmClip;
    public float bgmVolume;

    public enum NOOBSFX {Reload, Shoot, Error};

    [Header("SFX")]
    public AudioClip[] sfxClips;
    AudioSource[] sfxSources;
    public int maxSfxPlayer;
    public float sfxVolume;

    private void Awake()
    {
        if(instance == null) { instance = this; }

        Init();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.clip = bgmClip;
        bgmSource.playOnAwake = true;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxSources = new AudioSource[maxSfxPlayer];

        for(int i = 0; i <  sfxClips.Length; i++)
        {
            sfxSources[i] = sfxObject.AddComponent<AudioSource>();
            sfxSources[i].playOnAwake = false;
            sfxSources[i].volume = sfxVolume;
        }
    }

    public void PlaySfx(NOOBSFX sfx)
    {
        for(int i = 0; i < maxSfxPlayer; i++)
        {
            if (!sfxSources[i].isPlaying)
            {
                sfxSources[i].clip = sfxClips[(int)sfx];
                sfxSources[i].Play();
                break;
            }
        }
      
    }

}
