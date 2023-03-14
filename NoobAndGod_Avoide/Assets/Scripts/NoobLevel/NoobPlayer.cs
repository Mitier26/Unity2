using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobPlayer : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 1f;
    public void LeftButtoon()
    {
        transform.position = new Vector2(transform.position.x - moveDistance, transform.position.y);
    }

    public void RightButton()
    {
        transform.position = new Vector2(transform.position.x + moveDistance, transform.position.y);
    }
}
