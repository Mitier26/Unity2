using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Vector3 center;
    [SerializeField] private float startSpeed;
    [SerializeField] AudioClip moveClip, loseClip, winClip;

    private float speed, startRadius;

    private void Awake()
    {
        speed = startSpeed;
        startRadius = Vector3.Distance(transform.position, center);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            speed *= -1f;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }

    private void FixedUpdate()
    {
        Vector3 down = transform.position - center;
        Vector3 forward = Vector3.Cross(down, Vector3.forward).normalized;
        Vector3 temp = transform.position;
        temp += speed * Time.fixedDeltaTime * forward;

        float currentRadius = Vector3.Distance(temp, center);

        if (currentRadius > startRadius)
        {
            float extraRadius = currentRadius - startRadius;
            Vector3 offset = (temp - center).normalized;
            temp -= extraRadius * offset;
        }
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance.UpdateScore();
            AudioManager.Instance.PlaySound(winClip);
            return;
        }
        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            GameManager.Instance.EndGame();
            AudioManager.Instance.PlaySound(loseClip);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
