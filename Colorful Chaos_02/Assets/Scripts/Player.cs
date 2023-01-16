using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int colorID;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = GamePlayManager.Instance.colors[colorID];
    }
}
