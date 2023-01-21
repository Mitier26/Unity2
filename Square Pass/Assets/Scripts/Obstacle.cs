using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime;

    [SerializeField] private List<Vector3> obstacleSpawnPos;

    private bool hasGameFinished;

    private void Start()
    {
        hasGameFinished = false;

         Vector3 spawnPos;
         int posIndex;

        posIndex = Random.Range(0, this.obstacleSpawnPos.Count);
        spawnPos = this.obstacleSpawnPos[posIndex];
        transform.position = spawnPos;

        bool isShort = Random.Range(0, 3) == 0;
        if(isShort)
        {
            var col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(col.size.x - 1.6f, col.size.y);
            col.offset = new Vector2(col.offset.x - 0.8f, col.offset.y);
            GetComponent<SpriteRenderer>().size = col.size;
        }

        if(transform.position.x > 0f)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1f;
            transform.localScale = temp;
        }
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

        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.down;

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
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            transform.localScale = startScale + timeElapsed * scaleOffset;
            yield return updateTime;
        }

        Destroy(gameObject);
    }
}
