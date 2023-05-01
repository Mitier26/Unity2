using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBackgroundMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
