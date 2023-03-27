using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProAudioManager : MonoBehaviour
{
    public static ProAudioManager instance;

    public enum PROSFX { Box, Ball, Water, Death, Get, Fly, Coin }

    [SerializeField]
    private Transform player;                   // 플레이어의 위치
    [SerializeField]
    private AudioClip[] clips;                  // 출력할 오디오 종류

    [SerializeField]
    private int maxAudioSourceCount = 10;       // 최대로 출력할 사운드의 수
    [SerializeField]
    private GameObject[] audioPlayers;          // 사운드를 플레이하는 것


    private void Awake()
    {
        if (instance == null) instance = this;

        // 배열을 초기화 한다.
        audioPlayers = new GameObject[maxAudioSourceCount];

        for(int i = 0; i< maxAudioSourceCount; i++)
        {
            // 빈오브젝트를 생성하고 이름을 붙이고 AudioSource 를 추가한다.
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
                // 거리에 따라 소리의 크기를 변경하기 위한 것
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
