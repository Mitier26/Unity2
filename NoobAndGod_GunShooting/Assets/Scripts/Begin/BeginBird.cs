using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBird : MonoBehaviour
{
    // ���� �ൿ�� �����.
    // ���� �Ʒ��� �̵����� �ʴ´�.
    // ���� ��� �̵��ϸ� ���⸸ �����Ѵ�.
    // �¿� �̵��� ���� scale �� �����Ѵ�.
    // �����Ǹ� �˾Ƽ� �ൿ�Ѵ�.
    // ó�� ���� �¿�� ������ ���� ���� �ؾ� �Ѵ�.
    // aim
    // �ٴڿ� ���� �� ���� ���� �ؾ� �Ѵ�.
    // -6 ~ 6 ����

    // �̵� �ӵ��� ������ ���� �ٸ��� ���� ���� �ִ�.
    public float moveSpeed;             // ���� �̵� �ӵ�, ������ ������ �޾ƾ� �Ѵ�.
    public float normalSpeed;
    public float dashSpeed;

    public float changeTime;            // ������ �ٲٴ� �ð�
    public float currentTime = 0;       // ���� �۵� �ð�

    private Vector3 direction;          // ���� ����

    private void Start()
    {
        MovingSetting();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > changeTime)
        {
            currentTime = 0;
            MovingSetting();
        }

        Move();
    }

    private void MovingSetting()
    {
        ChangeTime();
        SetDirection();
    }

    // ���� ������ �ٲٴ� �ð��� �����Ѵ�.
    private void ChangeTime()
    {
        changeTime = Random.Range(1f, 2.5f);

        moveSpeed = normalSpeed;
    }

    // �̵�
    private void Move()
    {
        // ���� ȭ�� ���� ���� �ų� ȭ���� �¿�� ������ �Ⱥ��̰� �Ѵ�.
        if(transform.position.y >= 5.5f || transform.position.x < -9.5 || transform.position.x > 9.5)
        {
            gameObject.SetActive(false);
        }

        transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // ���� �̵� ������ ���Ѵ�.
    private void SetDirection()
    {
        // ���� �������� �̵������� �Ʒ��δ� �̵����� �ʰ� �Ѵ�.
        float x = Random.Range(-1f, 1f);
        float y = Random.Range( 0.2f, 0.8f);

        direction = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �Ǹ� ���� Ȯ���� ������ �̵��Ѵ�.
        if(collision.CompareTag("Player"))
        {
            int rate = Random.Range(0, 10);

            if(rate < 2)
            {
                SetDirection();
                moveSpeed = dashSpeed;
            }
        }
    }
}
