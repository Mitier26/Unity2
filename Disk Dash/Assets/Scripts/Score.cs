using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxOffset, destroyTime, rotateSpeed;

    private bool hasGameFinished;

    [SerializeField] private List<Vector3> spawnPos;

    private void Start()
    {
        hasGameFinished = false;

        transform.position = spawnPos[UnityEngine.Random.Range(0, spawnPos.Count)];
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

        transform.position += moveSpeed * Time.fixedDeltaTime * Vector3.up;
        transform.Rotate(rotateSpeed * Time.fixedDeltaTime * Vector3.forward);
        
        if (transform.position.x < maxOffset)
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
