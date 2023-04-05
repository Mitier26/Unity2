using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGameManager : MonoBehaviour
{
    public static GodGameManager Instance;

    public int characterId;         // 선택된 케릭터의 번호

    [SerializeField]
    private GameObject player;      // 플레이어

    public bool isOpening;          // 게임의 오프닝
    public bool isStart;            // 게임의 시작


    [SerializeField]                // 플레이어의 그림을 바꾸어줄 Animator
    private RuntimeAnimatorController[] animationControllers;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;


        isOpening = false;
        isStart = false;

    }

    public void GameStart(int id, Vector2 pos)
    {
        characterId = id;
        isStart = true;
        player.GetComponent<Animator>().runtimeAnimatorController = animationControllers[characterId];
        player.transform.position = pos;
        player.SetActive(true);
    }
}
