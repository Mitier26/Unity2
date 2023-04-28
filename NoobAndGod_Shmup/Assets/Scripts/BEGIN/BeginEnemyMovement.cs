using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginEnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private void Update()
    {
        transform.Translate(Vector3.down * 2f * Time.deltaTime);
    }
}
