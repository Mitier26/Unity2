using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset;

    [SerializeField] private float minSize, maxSize;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Transform scoreTransform;

    private void Start()
    {
        float offset = maxSize- minSize;
        float currentSize = minSize + Random.Range(0, 5) * 0.25f * offset;
        scoreTransform.localPosition = (currentSize + 1.6f) * Vector3.up;

        sr.size = new Vector2(sr.size.x, currentSize);
        col.size = sr.size;
        col.offset = new Vector2(0, currentSize / 2f);
    }


    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.left;

        if(transform.position.x < maxOffset)
        {
            Destroy(gameObject);
        }
    }
}
