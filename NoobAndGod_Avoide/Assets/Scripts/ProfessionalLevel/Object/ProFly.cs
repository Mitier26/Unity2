using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProFly : ProObject
{
    Transform player;                       // 플레이어 위치 확인용
    private Rigidbody2D rb;                 

    public Vector2 direction;               // 이동 방향
    public float startPositionX;            // 생성 초기 위치
    [SerializeField]
    private float attackSpeed;              // 공격 속도

    [Header("Detection")]
    private bool isTargeting = false;       // 플레이어 감지되었는지
    [SerializeField]
    LineRenderer lineRenderer;              // 인디케이터용 
    [SerializeField]
    private GameObject detector;            // 자식 오브젝트
    private float detectTime;               // 플레이어가 범위에 들어 왔을 때 공격 대기 시간

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
            // Fly와 플레어이 사이에 선을 만들어 준다
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
