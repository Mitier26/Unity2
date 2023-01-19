using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveTime, maxOffsetY;

    private float currentMovePosition;
    private float moveSpeed;
    private Vector3 startPos, endPos;

    private bool canMove;
    private bool canShoot;

    [SerializeField] private AudioClip moveClip, pointClip, scoreClip, loseClip;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        currentMovePosition = 0.5f;
        canMove = false;
        canShoot = false;
        moveSpeed = 1 / moveTime;

        Vector3 temp = transform.position;
        temp.y = -maxOffsetY;
        startPos = temp;
        temp.y = maxOffsetY;
        endPos = temp;
    }

    private void OnEnable()
    {
        GameManager.GameStarted += GameStarted;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= GameStarted;
    }

    private void GameStarted()
    {
        canShoot = true;
        canMove = true;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            moveSpeed *= -1f;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }


    private void FixedUpdate()
    {
        if (!canMove) return;

        currentMovePosition += moveSpeed * Time.fixedDeltaTime;

        if(currentMovePosition < 0f || currentMovePosition > 1f)
        {
            moveSpeed *= -1f;
        }

        transform.position = startPos + currentMovePosition * (endPos - startPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance.UpdateScore();
            AudioManager.Instance.PlaySound(scoreClip);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3f);
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            Destroy(gameObject);
        }
    }
}
