using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 사운드 출력을 위한 것
    // 다른곳에서 사용할 수 있도록 싱글톤으로 만든다.
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioSource _effectSource;

    // 사운드를 출력한다.
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

}
