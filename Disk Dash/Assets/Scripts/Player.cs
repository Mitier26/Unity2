using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        isSlow = true;
        speedMagnitude = 1f;
        speedMultiplier = slowMoveSpeedMultiplier;

        currentRotateValue = 0f;
        rotateMagnitude = 1f;
        rotateSpeedMultiplier = slowRotateSpeedMultiplier;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            isSlow = !isSlow;
            AudioManager.Instance.PlaySound(moveClip);
            speedMultiplier = isSlow ? slowMoveSpeedMultiplier : faseMoveSpeedMultiplier;
            rotateSpeedMultiplier = isSlow ? slowRotateSpeedMultiplier : faseMoveSpeedMultiplier;
        }
    }

    [SerializeField] private float startSpeed;
    [SerializeField] private float boundsX;
    [SerializeField] private float faseMoveSpeedMultiplier, slowMoveSpeedMultiplier;

    private float speedMagnitude;
    private float speedMultiplier;
    private bool isSlow;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float fastRotateSpeedMultiplier, slowRotateSpeedMultiplier;

    private float currentRotateValue;
    private float rotateMagnitude;
    private float rotateSpeedMultiplier;

    private Vector3 direction;

    private void FixedUpdate()
    {
        if (!canMove) return;

        transform.position += (speedMagnitude * speedMultiplier * startSpeed * Time.fixedDeltaTime * Vector3.right);

        currentRotateValue += (rotateMagnitude * rotateSpeedMultiplier * rotateSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(0, 0, currentRotateValue);

        if(transform.position.x < -boundsX || transform.position.x > boundsX)
        {
            speedMagnitude *= -1f;

            AudioManager.Instance.PlaySound(pointClip);

            if(currentRotateValue > 360f || currentRotateValue < 0f)
            {
                rotateMagnitude *= -1f;
            }
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
