using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector2 moveDirection;

    private void Awake()
    {
        moveDirection = Vector2.up;
        StartCoroutine(ScaleUp());
    }

    private void OnEnable()
    {
        GamePlayManager.Instance.GameEnd += GameEnded;
    }

    private void OnDisable()
    {
        GamePlayManager.Instance.GameEnd -= GameEnded;
    }

    [SerializeField] private GameObject clickParticle, scoreParticle, playerPerticle;
    [SerializeField] private AudioClip moveClip, scoreClip;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlaySound(moveClip);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            moveDirection = (mousePos2D - (Vector2)transform.position).normalized;

            Destroy(Instantiate(clickParticle, new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity), 1f);
        }

        float cosAngle = Mathf.Acos(moveDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, cosAngle * (moveDirection.y < 0f ? -1f : 1f));
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)(moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Side"))
        {
            moveDirection.x *= -1f;
        }

        if (collision.CompareTag("Top"))
        {
            moveDirection.y *= -1f;
        }

        if (collision.CompareTag("Score"))
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(Instantiate(scoreParticle, collision.gameObject.transform.position, Quaternion.identity), 1f);
            GamePlayManager.Instance.UpdateScore();
            StartCoroutine(ScoreDestroy(collision.gameObject));
        }
    }

    [SerializeField] private AnimationClip scoreDestroyClip;

    private IEnumerator ScoreDestroy(GameObject obj)
    {
        obj.GetComponent<Collider2D>().enabled = false;
        obj.GetComponent<Animator>().Play(scoreDestroyClip.name, -1, 0f);
        yield return new WaitForSeconds(scoreDestroyClip.length);
        Destroy(obj);
    }

    [SerializeField] private float animationTime;
    [SerializeField] private AnimationCurve scaleUpCurve, scaleDownCurve;

    private IEnumerator ScaleUp()
    {
        float timeElapsed = 0f;
        float speed = 1 / animationTime;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        Vector3 scaleOffset = endScale - startScale;

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            transform.localScale = startScale + scaleUpCurve.Evaluate(timeElapsed) * scaleOffset;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    private IEnumerator ScaleDown()
    {
        Destroy(Instantiate(playerPerticle, transform.position, Quaternion.identity), 1f);

        float timeElapsed = 0f;
        float speed = 1 / animationTime;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;
        Vector3 scaleOffset = endScale - startScale;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            transform.localScale = startScale + scaleUpCurve.Evaluate(timeElapsed) * scaleOffset;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void GameEnded()
    {
        StartCoroutine(ScaleDown());
    }
}
