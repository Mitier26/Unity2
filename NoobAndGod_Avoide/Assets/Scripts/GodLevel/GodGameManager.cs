using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGameManager : MonoBehaviour
{
    public static GodGameManager Instance;

    public int characterId;         // ���õ� �ɸ����� ��ȣ

    [SerializeField]
    private GameObject player;      // �÷��̾�

    public bool isOpening;          // ������ ������
    public bool isStart;            // ������ ����


    [SerializeField]                // �÷��̾��� �׸��� �ٲپ��� Animator
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
