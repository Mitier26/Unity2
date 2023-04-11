using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGhost2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostPrefab;         // ÀÜ»ó È¿°ú ¿ÀºêÁ§Æ®

    public GodPlayer2 player;              // ÇÃ·ÀÀÌ¾î

    [SerializeField]
    private float ghostDelay;               // ÀÜ»ó µô·¹ÀÌ
    private float ghostDelta;               // ÀÜ»ó ½Ã°£

    [SerializeField]
    private Color ghostColor;               // ÀÜ»ó »ö


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
            go.transform.rotation = transform.rotation;
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
