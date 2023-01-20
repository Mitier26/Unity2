using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private bool canMove;
    private bool canShoot;

    [SerializeField] private AudioClip moveClip, pointClip, scoreClip, loseClip;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        canMove = false;
        canShoot = false;

    }

    private void OnEnable()
    {
        GameManager.Instance.GameStarted += GameStarted;
        GameManager.Instance.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStarted -= GameStarted;
        GameManager.Instance.GameEnded -= OnGameEnded;
    }

    private void GameStarted()
    {
        canShoot = true;
        canMove = true;

        totalMovePos = movePositions.Count;
        moveStartIndex = 0;
        moveMagnitude = 1;
        moveEndIndex = (moveStartIndex + 1) % totalMovePos;
        moveStartPos = movePositions[moveStartIndex];
        moveEndPos = movePositions[moveEndIndex];
        moveDirection = (moveEndPos - moveStartPos).normalized;
        moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
        currentMoveDistance = moveDistance / 2f;
        transform.position = moveStartPos + currentMoveDistance * moveDirection;

    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            moveMagnitude *= -1f;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }

    [SerializeField] private List<Vector3> movePositions;
    [SerializeField] private float moveSpeed;

    private Vector3 moveStartPos, moveEndPos, moveDirection;
    private float moveDistance, moveMagnitude;
    private float currentMoveDistance;
    private int moveStartIndex, moveEndIndex, totalMovePos;

    private void FixedUpdate()
    {
        if (!canMove) return;

        if (currentMoveDistance > moveDistance)
        {
            currentMoveDistance = 0f;
            moveStartIndex = (moveStartIndex + 1) % totalMovePos;
            moveEndIndex = (moveStartIndex + 1) % totalMovePos;
            moveStartPos = movePositions[moveStartIndex];
            moveEndPos = movePositions[moveEndIndex];
            moveDirection = (moveEndPos - moveStartPos).normalized;
            moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
        }
        else if (currentMoveDistance < 0f)
        {
            moveStartIndex = (moveStartIndex - 1 + totalMovePos) % totalMovePos;
            moveEndIndex = (moveStartIndex + 1) % totalMovePos;
            moveStartPos = movePositions[moveStartIndex];
            moveEndPos = movePositions[moveEndIndex];
            moveDirection = (moveEndPos - moveStartPos).normalized;
            moveDistance = Vector3.Distance(moveEndPos, moveStartPos);
            currentMoveDistance = moveDistance;
        }


        currentMoveDistance += moveSpeed * moveMagnitude * Time.fixedDeltaTime;
        transform.position = moveStartPos + currentMoveDistance * moveDirection;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tags.SCORE) &&
            collision.gameObject.GetComponent<Score>().Id == GameManager.Instance.CurrentScoreId)
        {
            GameManager.Instance.UpdateScore();
            collision.gameObject.GetComponent<Score>().PlayScoreAnimation();
            AudioManager.Instance.PlaySound(scoreClip);
        }
        else if (collision.CompareTag(Constants.Tags.SCORE) &&
            collision.gameObject.GetComponent<Score>().Id != GameManager.Instance.CurrentScoreId)
        {
            AudioManager.Instance.PlaySound(pointClip);
        }

        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3f);
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
        }
    }

    [SerializeField] private float destroyTime;

    public void OnGameEnded()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Rescale());
    }

    private IEnumerator Rescale()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        Vector3 scaleOffset = endScale - startScale;
        float timeElapsed = 0;
        float speed = 1 / destroyTime;
        var updateTime = new WaitForFixedUpdate();
        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }
}
