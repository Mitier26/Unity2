using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMagicObj : MonoBehaviour
{
    Rigidbody2D rb;
    public bool ison;
    public Vector2 clickPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseEnter()
    {
        ison = true;
    }

    private void OnMouseExit()
    {
        ison = false;
    }

    private void OnMouseDown()
    {
        if(ison)
        {
            clickPos = transform.position -  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        rb.velocity = Vector2.zero;
    }


    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        transform.position = mousePos + clickPos;
    }

}
