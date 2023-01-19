using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Vector3> movePosition;
    [SerializeField] private float moveTime;

    private float currentMovePosition;
    private float moveSpeed;

    private bool canMove;
    private bool canShoot;

    [SerializeField] private AudioClip moveClip, pointClip, scoreClip, loseClip;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        currentMovePosition = 0;
        canMove = false;
        canShoot = false;
        moveSpeed = 1 / moveTime;
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

    private int startIndex, endIndex;
    private float moveDistance;
    private Vector3 startPos, endPos;

    private void FixedUpdate()
    {
        if (!canMove) return;

        currentMovePosition += moveSpeed * Time.fixedDeltaTime;

        if(currentMovePosition < 0f)
        {
            currentMovePosition = movePosition.Count;
        }

        if(currentMovePosition > movePosition.Count)
        {
            currentMovePosition = 0;
        }

        startIndex = Mathf.FloorToInt(currentMovePosition) % movePosition.Count;
        endIndex = (startIndex + 1) % movePosition.Count;
        moveDistance = (currentMovePosition % movePosition.Count) - startIndex;

        startPos = movePosition[startIndex];
        endPos = movePosition[endIndex];

        transform.position = startPos + moveDistance * (endPos - startPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            if(collision.gameObject.GetComponent<Score>().currentId == GameManager.Instance.currentTargetIndex)
            {
                GameManager.Instance.UpdateScore();
                AudioManager.Instance.PlaySound(scoreClip);
            }
            else
            {
                AudioManager.Instance.PlaySound(pointClip);
            }

        }
        

        if(collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3f);
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            Destroy(gameObject);
        }
    }
}
