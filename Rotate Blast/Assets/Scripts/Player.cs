using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector3 startCenterPos;
    [SerializeField]
    private float rotateRadius, rotateSpeed, moveSpeed;

    private Vector3 centerPos;

    private bool canMove;
    private bool canRotate;
    private bool canShoot;

    private float currentRotateAngle;
    private Vector3 moveDirection;

    [SerializeField]
    private GameObject whiteExplosionPrefab, yellowExploaionPrefab;

    public ParticleSystem.EmissionModule  trailPaticle;

    [SerializeField]
    private AudioClip moveClip, pointClip, loseClip;

    private void Awake()
    {
        canRotate = true;
        canShoot = true;
        canMove = false;
        centerPos = startCenterPos;
        currentRotateAngle = 0f;
        trailPaticle = GetComponent<ParticleSystem>().emission;
        trailPaticle.enabled = false;
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
        canRotate = true;
    }

    private void Update()
    {
        if(canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(canRotate)
        {
            currentRotateAngle += rotateSpeed * Time.fixedDeltaTime;
            moveDirection = new Vector3(Mathf.Cos(currentRotateAngle * Mathf.Deg2Rad), Mathf.Sin(currentRotateAngle * Mathf.Deg2Rad), 0).normalized;
            transform.position = centerPos + rotateRadius * moveDirection;

            if (currentRotateAngle >= 360f) currentRotateAngle = 0f;
        }
        else
        {
            transform.position += moveSpeed * Time.fixedDeltaTime * moveDirection;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            EndGame();
        }
        if (collision.CompareTag(Constants.Tags.SCORE))
        {
            AudioManager.Instance.PlaySound(pointClip);
            Destroy(Instantiate(yellowExploaionPrefab, transform.position, Quaternion.identity), 3.2f);
            moveSpeed *= -1f;
            GameManager.Instance.UpdateScore(collision.gameObject.GetComponent<Score>().currentId);
        }
        if (collision.CompareTag(Constants.Tags.CENTER))
        {
            centerPos = collision.gameObject.transform.position;
            canMove = false;
            canRotate = true;
            canShoot = true;
            Vector3 direction = (transform.position - centerPos).normalized;
            float cosAngle = Mathf.Acos(direction.x) * Mathf.Rad2Deg;
            float sinAngle = Mathf.Asin(direction.x) * Mathf.Rad2Deg;

            //float tangent = (transform.position.y - centerPos.y) / (transform.position.x - centerPos.x);
            //currentRotateAngle = Mathf.Atan(tangent);

            currentRotateAngle = cosAngle * (sinAngle < 0 ? -1f : 1f);
            
            moveSpeed *= -1f;
            rotateSpeed *= -1f;
         
        }
    }

    private void EndGame()
    {
        AudioManager.Instance.PlaySound(loseClip);
        Destroy(Instantiate(whiteExplosionPrefab, transform.position, Quaternion.identity), 5f);
        GameManager.Instance.EndGame();
        Destroy(gameObject);
    }

    private void Shoot()
    {
        trailPaticle.enabled = true;
        AudioManager.Instance.PlaySound(moveClip);
        canRotate = false;
        canShoot = false;
        canMove = true;
        moveDirection = (transform.position - centerPos).normalized;
    }
}