using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Vector3 startPos, endPos, startScale, endScale;
    [SerializeField] private float startSpeed, timeToScale;
    [SerializeField] private AudioClip moveClip, pointClip, winClip, loseClip;
    private float speed;

    private void Awake()
    {
        speed = startSpeed;
    }

    private void OnEnable()
    {
        GameManager.GameStarted += GameStarted;
        GameManager.GameEnded += GameEnded;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= GameStarted;
        GameManager.GameEnded -= GameEnded;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySound(moveClip);
            speed *= -1f;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector3.right);
        if(transform.position.x > endPos.x || transform.position.x < startPos.x)
        {
            speed *= -1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            GetComponent<Collider2D>().enabled = false;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        if (collision.CompareTag(Constants.Tags.SCORE))
        {
            AudioManager.Instance.PlaySound(pointClip);
            GameManager.Instance.UpdateScore();
            Destroy(collision.gameObject);
            return;
        }
    }

    private void GameStarted()
    {
        StartCoroutine(Scale(transform, startScale, endScale, timeToScale));
    }

    private void GameEnded()
    {
        StartCoroutine(Scale(transform, endScale, Vector3.zero, timeToScale));
        Destroy(gameObject, timeToScale);
    }

    public IEnumerator Scale(Transform target, Vector3 startScale, Vector3 endScale, float timeToFinish)
    {
        target.localScale = startScale;

        float timeElapsed = 0;
        float speed = 1 / timeToFinish;

        Vector3 offset = endScale - startScale;

        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            target.localScale = startScale + timeElapsed * offset;
            yield return null;
        }

        target.localScale = endScale;
    }
}