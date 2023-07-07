using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobPlayer : MonoBehaviour
{
    NoobGamaManager gm;
    NoobTarget target = null;
    RaycastHit2D hit;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<NoobGamaManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                target = hit.collider.GetComponent<NoobTarget>();
                target.Reposition();
                gm.Score++;
            }
        }
    }
}
