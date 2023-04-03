using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGhost2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ghostPrefab;         // ¿‹ªÛ »ø∞˙ ø¿∫Í¡ß∆Æ

    private GodPlayer2 player;              // «√∑¿¿ÃæÓ

    [SerializeField]
    private float ghostDelay;               // ¿‹ªÛ µÙ∑π¿Ã
    private float ghostDelta;               // ¿‹ªÛ Ω√∞£

    [SerializeField]
    private Color ghostColor;               // ¿‹ªÛ ªˆ

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
