using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveTime, maxOffsetY;

    private float currentRotateAngle;
    private float rotateSpeed;

    private bool canMove;
    private bool canShoot;

    [SerializeField] private AudioClip moveClip, pointClip, scoreClip, loseClip;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        currentRotateAngle = 0f;
        canMove = false;
        canShoot = false;
        rotateSpeed = 90f / moveTime;

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
            rotateSpeed *= -1f;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }


    private void FixedUpdate()
    {
        if (!canMove) return;

        currentRotateAngle += rotateSpeed * Time.fixedDeltaTime;
        
        transform.rotation = Quaternion.Euler(0, 0, currentRotateAngle);

        if(currentRotateAngle < 0f)
        {
            currentRotateAngle = 360f;
        }
        if(currentRotateAngle > 360f)
        {
            currentRotateAngle = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance.UpdateScore();
            AudioManager.Instance.PlaySound(scoreClip);
            collision.GetComponent<Obstacle>().OnGameEnded();
        }

        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            Destroy(Instantiate(explosionPrefab, transform.GetChild(0).position, Quaternion.identity), 3f);
            Destroy(Instantiate(explosionPrefab, transform.GetChild(1).position, Quaternion.identity), 3f);
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            Destroy(gameObject);
        }
    }
}
