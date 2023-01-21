using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private bool canMove;
    private bool canShoot;
    private bool isSlow;

    [SerializeField] private AudioClip moveClip, pointClip, scoreClip, loseClip;

    [SerializeField] private GameObject explosionPrefab;

    private void Awake()
    {
        canMove = false;
        canShoot = false;

    }

    private void OnEnable()
    {
        GameManager.GameStarted += GameStarted;
        GameManager.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= GameStarted;
        GameManager.GameEnded -= OnGameEnded;
    }

    private void GameStarted()
    {
        canShoot = true;
        canMove = true;

        moveSpeed = 1 / moveTime;
        currentMoveDistance = 0.5f;
        moveOffset = moveEndPos - moveStartPos;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            moveSpeed *= -1f;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }

    [SerializeField] private float moveTime;
    [SerializeField] private Vector3 moveStartPos, moveEndPos;

    private Vector3 moveOffset;
    private float moveSpeed;
    private float currentMoveDistance;


    private void FixedUpdate()
    {
        if (!canMove) return;

        if(currentMoveDistance > 1f || currentMoveDistance < 0f) 
        {
            moveSpeed *= -1f;
            currentMoveDistance = currentMoveDistance > 0.5f ? 1f : 0f;
        }

        currentMoveDistance += moveSpeed * Time.fixedDeltaTime;
        transform.position = moveStartPos + currentMoveDistance * moveOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(Constants.Tags.SCORE))
        {
            collision.gameObject.GetComponent<Score>().OnGameEnded();
            GameManager.Instance.UpdateScore();
            AudioManager.Instance.PlaySound(scoreClip);
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
