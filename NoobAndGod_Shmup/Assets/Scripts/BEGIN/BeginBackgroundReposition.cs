using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBackgroundReposition : MonoBehaviour
{
    [SerializeField] private float limitY = 12f;

    private void Update()
    {
        if(transform.position.y < -limitY)
        {
            transform.Translate(0, limitY * 2, 0, Space.Self);
        }
    }
}
