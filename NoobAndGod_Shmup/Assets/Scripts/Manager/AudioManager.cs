using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private float bgmVolume;
    private AudioSource bgmSource;

    [Header("SFX")]
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;
    [SerializeField] private int channels;
    [SerializeField] private AudioSource[] sfxSources;
    private int channelIndex;

    // 이거 수정해야 한다.
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        //bgmSource.clip = bgmClips[0];

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxSources = new AudioSource[channels];

        for(int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i] = sfxObject.AddComponent<AudioSource>();
            sfxSources[i].playOnAwake = false;
            sfxSources[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    public void PlaySfx(SFX sfx)
    {
        for(int i = 0; i < sfxSources.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxSources.Length;

            if (sfxSources[loopIndex].isPlaying)
                continue;

            sfxSources[loopIndex].clip = sfxClips[(int)sfx];
            sfxSources[loopIndex].Play();
            break;
        }
    }

}
