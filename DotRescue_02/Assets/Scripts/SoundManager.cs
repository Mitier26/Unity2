using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ���� ����� ���� ��
    // �ٸ������� ����� �� �ֵ��� �̱������� �����.
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

    // ���带 ����Ѵ�.
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

}
