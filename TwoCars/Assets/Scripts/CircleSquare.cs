using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSquare : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigid;
    private void Start()
    {
        speed = 10f;
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -speed);
    }
}
