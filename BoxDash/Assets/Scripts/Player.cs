using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
        canRotate = false;

        currentRotateAngle = 90f;
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

        minRotateAngle = startRotateMinAngle;
        maxRotateAngle = startRotateMaxAngle;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
            AudioManager.Instance.PlaySound(moveClip);
        }
    }

    private void Shoot()
    {
        canRotate = false;
        canShoot = false;

        moveDirection = new Vector3(Mathf.Cos(currentRotateAngle * Mathf.Deg2Rad),
            Mathf.Sin(currentRotateAngle * Mathf.Deg2Rad), 0f);
        currentRotateAngle = (currentRotateAngle + 180f) % 720f;
        rotateTransform.gameObject.SetActive(false);
    }

    [SerializeField] private Transform rotateTransform;
    [SerializeField] private float rotateSpeed, moveSpeed;
    [SerializeField] private float startRotateMinAngle, startRotateMaxAngle;

    private bool canRotate;
    private float currentRotateAngle;
    private Vector3 moveDirection;

    private float minRotateAngle, maxRotateAngle;

    private void FixedUpdate()
    {
        if (!canMove) return;

        if (canRotate)
        {
            if(currentRotateAngle > maxRotateAngle)
            {
                currentRotateAngle = maxRotateAngle;
                rotateSpeed *= -1f;
            }
            if (currentRotateAngle < minRotateAngle)
            {
                currentRotateAngle = minRotateAngle;
                rotateSpeed *= -1f;
            }

            currentRotateAngle += rotateSpeed * Time.fixedDeltaTime;
            rotateTransform.rotation = Quaternion.Euler(0, 0, currentRotateAngle);
        }
        else if (canMove)
        {
            transform.position += moveSpeed * Time.fixedDeltaTime * moveDirection;
        }

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

        if(collision.CompareTag(Constants.Tags.MOVE))
        {
            AudioManager.Instance.PlaySound(pointClip);
            canRotate = true;
            canShoot = true;
            rotateTransform.gameObject.SetActive(true);
            rotateTransform.rotation = Quaternion.Euler(0, 0, currentRotateAngle);
            var moveScript = collision.gameObject.GetComponent<Boundary>();
            minRotateAngle = moveScript.minAngle;
            maxRotateAngle = moveScript.maxAngle;
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
