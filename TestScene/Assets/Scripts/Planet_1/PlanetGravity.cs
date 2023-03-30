using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public GameObject planet;           // �༺

    Rigidbody2D rb;                     // ����
    
    public float gravityForce;          // �߷��� ��
    public float gravityDistance;       // �߷��� �Ÿ�

    float lookAngle;                    // ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ���ڿ� �༺���� �Ÿ�
        float dist = Vector3.Distance(gameObject.transform.position, planet.transform.position);

        // �༺���� ���ڱ����� ����
        Vector3 v = planet.transform.position - transform.position;
        
        // ���ڿ��� ���� ���ϴ� ���̴�.
        // �Ÿ��� ���� �߷��� ���� ���Ѵ�.
        rb.AddForce(v.normalized * (1.0f - dist / gravityDistance) * gravityForce);

        // ���ڸ� �༺�� �������� ȸ���ϴ� ���̴�.
        // ������ �̿��� ȸ������ ���Ѵ� ��
        lookAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
}
