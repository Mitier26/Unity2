using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int colorId;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = GamePlayManager.Instance.colors[colorId];
    }
}
