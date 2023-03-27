using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProAudioManager : MonoBehaviour
{
    public static ProAudioManager instance;

    public enum PROSFX { Box, Ball, Water, Death, Get, Fly, Coin }

    [SerializeField]
    private Transform player;                   // �÷��̾��� ��ġ
    [SerializeField]
    private AudioClip[] clips;                  // ����� ����� ����

    [SerializeField]
    private int maxAudioSourceCount = 10;       // �ִ�� ����� ������ ��
    [SerializeField]
    private GameObject[] audioPlayers;          // ���带 �÷����ϴ� ��


    private void Awake()
    {
        if (instance == null) instance = this;

        // �迭�� �ʱ�ȭ �Ѵ�.
        audioPlayers = new GameObject[maxAudioSourceCount];

        for(int i = 0; i< maxAudioSourceCount; i++)
        {
            // �������Ʈ�� �����ϰ� �̸��� ���̰� AudioSource �� �߰��Ѵ�.
            audioPlayers[i] = new GameObject("AudioPlayer");
            audioPlayers[i].transform.SetParent(transform);
            AudioSource source = audioPlayers[i].AddComponent<AudioSource>();
            source.volume = 0.1f;
            source.playOnAwake = false;
            source.minDistance = 10;
            source.maxDistance = 20;
        }
    }

    public void PlaySound(PROSFX sfx, Vector2 position)
    {
        for(int i = 0; i< maxAudioSourceCount; i++)
        {
            AudioSource source = audioPlayers[i].GetComponent<AudioSource>();

            if (!source.isPlaying)
            {
                // �Ÿ��� ���� �Ҹ��� ũ�⸦ �����ϱ� ���� ��
                float distance = Vector2.Distance(position, player.position);

                if(distance > source.maxDistance)
                {
                    source.volume = 0f;
                }
                else if( distance < source.minDistance)
                {
                    source.volume = 0.3f;
                }
                else
                {
                    source.volume = 0.3f - ((distance - source.minDistance) / (source.maxDistance - source.minDistance));
                }

                audioPlayers[i].transform.position = position;
                source.clip = clips[(int)sfx];
                source.Play();
                break;
            }
        }
    }
}
