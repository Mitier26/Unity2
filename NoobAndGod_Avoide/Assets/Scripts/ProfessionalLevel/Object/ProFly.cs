using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProFly : ProObject
{
    Transform player;                       // �÷��̾� ��ġ Ȯ�ο�
    private Rigidbody2D rb;                 

    public Vector2 direction;               // �̵� ����
    public float startPositionX;            // ���� �ʱ� ��ġ
    [SerializeField]
    private float attackSpeed;              // ���� �ӵ�

    [Header("Detection")]
    private bool isTargeting = false;       // �÷��̾� �����Ǿ�����
    [SerializeField]
    LineRenderer lineRenderer;              // �ε������Ϳ� 
    [SerializeField]
    private GameObject detector;            // �ڽ� ������Ʈ
    private float detectTime;               // �÷��̾ ������ ��� ���� �� ���� ��� �ð�

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
        isTargeting = false;
        
    }

    private IEnumerator Attack()
    {
        detectTime = 0;
        while (detectTime < 2f)
        {
            // Fly�� �÷����� ���̿� ���� ����� �ش�
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player.position);
            detectTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.enabled = false;
        direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * (attackSpeed * 2f);
        yield break;
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
