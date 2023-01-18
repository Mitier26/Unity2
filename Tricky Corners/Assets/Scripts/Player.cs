using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Vector3[] directions;
    [SerializeField] private float startSpeed;

    private float speed;
    private int speedMagnitude;
    private int speedDirectionIndex;

    [SerializeField] private AudioClip moveClip, pointClip, loseClip;
    [SerializeField] private AnimationClip destroyClip;

    private void Awake()
    {
        speed = startSpeed;
        speedMagnitude = 1;
        speedDirectionIndex = 0;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            speedDirectionIndex += speedMagnitude;
            if (speedDirectionIndex == -1) speedDirectionIndex = directions.Length - 1;
            if (speedDirectionIndex == directions.Length) speedDirectionIndex = 0;
            AudioManager.Instance.PlaySound(moveClip);
        }
    }

    private void FixedUpdate()
    {
        Vector3 temp = transform.position;
        temp += speed * speedMagnitude * Time.fixedDeltaTime * directions[speedDirectionIndex];
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            GameManager.Instance.EndGame();
            AudioManager.Instance.PlaySound(loseClip);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        if(collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance.UpdateScore();
            StartCoroutine(PlayCollisionAnimation(collision.gameObject));
            AudioManager.Instance.PlaySound(pointClip);
            if (Random.Range(0f, 1f) > 0.6) speedMagnitude *= -1;
            return;
        }
    }

    IEnumerator PlayCollisionAnimation(GameObject target)
    {
        target.GetComponent<Collider2D>().enabled = false;
        Animator targetAnimator = target.GetComponent<Animator>();
        targetAnimator.Play(destroyClip.name);
        yield return new WaitForSeconds(destroyClip.length);
        Destroy(target);

    }
}