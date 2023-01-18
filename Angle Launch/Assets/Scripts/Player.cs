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

    private float rotateAngle;
    private Vector3 moveDirection;

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private AudioClip moveClip, pointClip, loseClip;

    private void Awake()
    {
        canRotate = true;
        canShoot = true;
        canMove = false;
        centerPos = startCenterPos;
        rotateAngle = 0f;
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
            rotateAngle += rotateSpeed * Time.fixedDeltaTime;
            moveDirection = new Vector3(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad), 0).normalized;
            transform.position = centerPos + rotateRadius * moveDirection;

            if (rotateAngle >= 360f) rotateAngle = 0f;
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
            centerPos = collision.gameObject.transform.position;
            canMove = false;
            canRotate = true;
            canShoot = true;
            float tangent = (transform.position.y - centerPos.y) / (transform.position.x - centerPos.x);
            rotateAngle = Mathf.Atan(tangent);
            int id = collision.gameObject.GetComponent<Score>().Id;
            GameManager.Instance.UpdateScore(id);
        }

    }

    private void EndGame()
    {
        AudioManager.Instance.PlaySound(loseClip);
        Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 5f);
        GameManager.Instance.EndGame();
        Destroy(gameObject);
    }

    private void Shoot()
    {
        canRotate = false;
        canShoot = false;
        canMove = true;
        moveDirection = (transform.position - centerPos).normalized;
    }
}