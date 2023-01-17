using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioClip clickClip;

    private bool isSoundMuted;
    private bool IsSoundMuted
    {
        get
        {
            isSoundMuted = (PlayerPrefs.HasKey(Constants.DATA.SETTING_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTING_SOUND) : 1) == 0;
            return isSoundMuted;
        }
        set
        {
            isSoundMuted = value;
            PlayerPrefs.SetInt(Constants.DATA.SETTING_SOUND, isSoundMuted ? 0 : 1);
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayerPrefs.SetInt(Constants.DATA.SETTING_SOUND, IsSoundMuted ? 0 : 1);
        effectSource.mute = IsSoundMuted;
    }

    public void AddButtonSound()
    {
        var buttons = FindObjectsOfType<Button>(true);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => { PlaySound(clickClip); });    
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void ToggleSound()
    {
        effectSource.mute = IsSoundMuted;
    }
}
