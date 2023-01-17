using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bgObject, directionObject, greenExplosionPrefab, redExplosionPrefab;

    private Transform directionTransform;

    private bool hasGameStarted;
    private bool canRotate;

    [SerializeField] private float maxOffset, timeForHalfCycle, intialOffset;

    private bool canShoot;
    private float shootCounter, shootCounterSpeed;

    private bool canMove;
    [SerializeField] private float moveSpeed, startPosY;
    private Vector3 moveDirection;

    [SerializeField] private AudioClip moveClip, loseClip, winClip;

    private bool canScore;

    private void Awake()
    {
        bgObject.SetActive(true);
        directionTransform = directionObject.transform;
        directionTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Start()
    {
        hasGameStarted = false;
        canRotate = true;

        canShoot = true;
        shootCounter = 0.5f;
        shootCounterSpeed = 1 / timeForHalfCycle;

        canMove = false;
        moveDirection = Vector3.zero;

        canScore = false;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySound(moveClip);

        canRotate = false;
        hasGameStarted = true;
        canShoot = false;
        canMove = true;
        canScore = true;

        float angle = maxOffset * (shootCounter - 0.5f) * 2f - intialOffset;
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized;
        directionObject.SetActive(false);
        bgObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!hasGameStarted) return;

        if(canRotate)
        {
            shootCounter += Time.fixedDeltaTime * shootCounterSpeed;

            if(shootCounter > 1f || shootCounter < 0f)
            {
                shootCounterSpeed *= -1f;
            }

            float angle = maxOffset * (shootCounter - 0.5f) * 2f;
            directionTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if(canMove)
        {
            Vector3 targetPos = transform.position + moveDirection;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

            if(transform.position.y < startPosY)
            {
                canMove = false;
                canRotate = true;
                canShoot = true;

                directionObject.SetActive(true);
                bgObject.SetActive(true);

                Vector3 tempPos = transform.position;
                tempPos.y = startPosY;
                transform.position = tempPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.DEATH))
        {
            AudioManager.Instance.PlaySound(loseClip);
            Destroy(collision.gameObject);
            Instantiate(redExplosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.EndGame();
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag(Constants.Tags.GOAL))
        {
            if (!canScore) return;

            AudioManager.Instance.PlaySound(winClip);
            Destroy(collision.gameObject);
            Instantiate(greenExplosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateScore();
            moveDirection *= -1f;
            return;
        }
    }
}
