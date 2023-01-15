using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip moveClip, loseClip, pointClip;

    [SerializeField] private GamePlayManager gm;
    [SerializeField] private GameObject explosionPrefab, scoreParticlePrefab;

    private bool canClick;

    private void Awake()
    {
        canClick = true;
        level = 0;
        currentRadius = startRadius;
    }

    private void Update()
    {
        if(canClick && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ChangeRadius());
            SoundManager.Instance.PlaySound(moveClip);
        }
    }

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform rotateTransform;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * currentRadius;
        float rotateValue = rotateSpeed * Time.fixedDeltaTime * 1;
        rotateTransform.Rotate(0, 0, rotateValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(loseClip);
            gm.GameEnded();
            return;
        }

        if(collision.CompareTag("Score"))
        {
            Destroy( Instantiate(scoreParticlePrefab, transform.position, Quaternion.identity), 2f);
            SoundManager.Instance.PlaySound(pointClip);
            gm.UpdateScore();
            collision.gameObject.GetComponent<Score>().ScoreAdded();
            return;
        }
    }

    [SerializeField] private float startRadius;
    [SerializeField] private float moveTime;

    [SerializeField] private List<float> rotateRadius;
    private float currentRadius;

    private int level;

    private IEnumerator ChangeRadius()
    {
        canClick = false;
        float moveStartRadius = rotateRadius[level];
        float moveEndRadius = rotateRadius[(level + 1) % rotateRadius.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / moveTime;
        float timeElasped = 0f;

        while(timeElasped < 1f)
        {
            timeElasped += speed * Time.deltaTime;
            currentRadius = moveStartRadius + timeElasped * moveOffset;
            yield return new WaitForFixedUpdate();
        }

        canClick = true;
        level = (level + 1) % rotateRadius.Count;
        currentRadius = rotateRadius[level];
    }
}
