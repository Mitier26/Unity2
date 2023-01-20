using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime;

    [SerializeField] private List<Vector3> obstacleSpawnPos;

    private Vector3 spawnPos;
    private int posIndex;

    private bool hasGameFinished;
    private bool isLeft;


    private void Start()
    {
        hasGameFinished = false;

        posIndex = Random.Range(0, obstacleSpawnPos.Count);
        spawnPos = obstacleSpawnPos[posIndex];
        isLeft = Random.Range(0, 2) == 0;
        spawnPos.x *= isLeft ? -1f : 1f;
        transform.position = spawnPos;
        moveSpeed *= isLeft ? -1f : 1f;

    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= OnGameEnded;
    }


    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.left;

        if(transform.position.x > maxOffset || transform.position.x < -maxOffset)
        {
            Destroy(gameObject);
        }

    }

    public void OnGameEnded()
    {
        GetComponent<Collider2D>().enabled = false;
        hasGameFinished = true;
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
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }

}
