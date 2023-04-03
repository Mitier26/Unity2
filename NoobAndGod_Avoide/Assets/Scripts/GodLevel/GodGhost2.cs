using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGhost2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostPrefab;         // �ܻ� ȿ�� ������Ʈ

    private GodPlayer2 player;              // �÷��̾�

    [SerializeField]
    private float ghostDelay;               // �ܻ� ������
    private float ghostDelta;               // �ܻ� �ð�

    [SerializeField]
    private Color ghostColor;               // �ܻ� ��

    private void Awake()
    {
        player = GetComponent<GodPlayer2>();
    }


    private void Update()
    {
        //if (player.inputX == 0) return;

        if(ghostDelta > ghostDelay)
        {
            ghostDelta = 0;
            GameObject go = Instantiate(ghostPrefab);
            go.GetComponent<SpriteRenderer>().color = ghostColor;
            go.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            go.transform.position = transform.position;
            go.transform.localScale = transform.localScale;

            if (player.inputX < 0)
                go.GetComponent<SpriteRenderer>().flipX = true;
            else if (player.inputX > 0)
                go.GetComponent<SpriteRenderer>().flipX = false;

                Destroy(go, 1f);
        }
        else
        {
            ghostDelta += Time.deltaTime;
        }
    }
}
