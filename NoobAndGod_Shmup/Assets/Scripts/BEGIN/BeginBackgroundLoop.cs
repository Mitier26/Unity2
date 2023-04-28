using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBackgroundLoop : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float basePositionY;

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (transform.localPosition.y < -11)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, basePositionY, 0);
        }
    }
}
