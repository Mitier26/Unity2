using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveTime, roTateRadius;

    [SerializeField] private Vector3 center;

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
        rotateSpeed = 360f / moveTime;

    }

    private void OnEnable()
    {
        GameManager.GameStarted += GameStarted;
        GameManager.Instance.ColorChanged += ColorChanged;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= GameStarted;
        GameManager.Instance.ColorChanged -= ColorChanged;
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

    private Vector3 direction;

    private void FixedUpdate()
    {
        if (!canMove) return;

        currentRotateAngle += rotateSpeed * Time.fixedDeltaTime;

        direction = new Vector3(Mathf.Cos(currentRotateAngle * Mathf.Deg2Rad), Mathf.Sin(currentRotateAngle * Mathf.Deg2Rad), 0);

        transform.position = center + roTateRadius * direction;

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
            collision.GetComponent<Score>().OnGameEnded();
        }

        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3f);
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            Destroy(collision.gameObject);
        }
    }

    private void ColorChanged(Color col)
    {
        GetComponent<SpriteRenderer>().color = col;
        var mm = GetComponent<ParticleSystem>().main;
        mm.startColor = col;
    }
}
