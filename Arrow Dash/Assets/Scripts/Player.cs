using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 startSpawnPos;
    [SerializeField] private float minX, maxX;
    [SerializeField] private float moveTime;
    [SerializeField] private ParticleSystem trailParticle;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip moveClip, pointClip, loseClip;

    private float speed;
    private bool canClick, canMove;

    private void Awake()
    {
        transform.position = startSpawnPos;
        trailParticle.Pause();
        canClick = false;
        canMove = false;
        speed = (maxX - minX) / moveTime;

    }

    private void OnEnable()
    {
        GameManager.Instance.GameStarted += GameStarted;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameStarted -= GameStarted;
    }

    private void GameStarted()
    {
        trailParticle.Play();
        canMove = true;
        canClick = true;
    }

    private void Update()
    {
        if (!canClick) return;
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySound(moveClip);
            speed *= -1f;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        transform.Translate(speed * Time.fixedDeltaTime * Vector3.right);
        if(transform.position.x < minX || transform.position.x > maxX) speed *= -1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            AudioManager.Instance.PlaySound(pointClip);
            collision.gameObject.GetComponent<Score>().DestroySprite();
            GameManager.Instance.UpdateScore();
            return;
        }

        if(collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            AudioManager.Instance.PlaySound(loseClip);
            GameManager.Instance.EndGame();
            GetComponent<Collider2D>().enabled = false;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            canMove = false;
            canClick = false;
            return;
        }
    }
}