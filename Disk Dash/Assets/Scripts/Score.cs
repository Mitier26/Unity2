using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float moveSpeed, rotateSpeed, maxOffset, destroyTime;

    [SerializeField] private List<Vector3> obstacleSpawnPos;

    private Vector3 moveDirection;

    private bool hasGameFinished;

    private void Start()
    {
        hasGameFinished = false;

        Vector3 spawnPos;
        int posIndex;

        posIndex = Random.Range(0, this.obstacleSpawnPos.Count);
        spawnPos = this.obstacleSpawnPos[posIndex];
        transform.position = spawnPos;
        moveDirection = -1 * spawnPos.normalized;
    }

    private void OnEnable()
    {
        GameManager.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= OnGameEnded;
    }


    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        transform.position += moveSpeed * Time.fixedDeltaTime * moveDirection;

        transform.Rotate(rotateSpeed * Time.fixedDeltaTime * Vector3.forward);

        if (transform.position.x > maxOffset || transform.position.x < -maxOffset)
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
        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }
}
