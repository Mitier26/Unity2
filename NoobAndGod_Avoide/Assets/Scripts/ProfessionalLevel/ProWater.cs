using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProWater : MonoBehaviour
{
    private Tilemap tile;
   

    private void Awake()
    {
        tile = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        ProAudioManager.instance.PlaySound(ProAudioManager.PROSFX.Water, collision.transform.position);

        if(collision.CompareTag("Player"))
        {
            tile.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tile.color = new Color(1, 1, 1, 1f);
        }
    }
}
