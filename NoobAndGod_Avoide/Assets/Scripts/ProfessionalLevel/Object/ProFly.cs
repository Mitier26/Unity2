using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProFly : ProObject
{
    Transform player;
    private Rigidbody2D rb;

    public Vector2 direction;
    public float startPositionX;
    [SerializeField]
    private float attackSpeed;

    [Header("Detection")]
    private bool isTargeting = false;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    private GameObject detector;
    private float detectTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetFly()
    {
        detector.SetActive(true);
        transform.localPosition = new Vector3(0, transform.position.y, 0);
        GetComponent<Rigidbody2D>().velocity = direction;
        attackSpeed = direction.magnitude;
    }

    private IEnumerator Attack()
    {
        while (detectTime < 2f)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player.position);
            detectTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
        direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * (attackSpeed * 2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTargeting)
        {
            lineRenderer.enabled = true;
            player = collision.transform;
            rb.velocity = Vector2.zero;
            detector.SetActive(false);
            isTargeting = true;
            StartCoroutine(Attack());

        }
    }
}
