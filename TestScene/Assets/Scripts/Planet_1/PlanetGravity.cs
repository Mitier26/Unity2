using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public GameObject planet;           // 행성

    Rigidbody2D rb;                     // 물리
    
    public float gravityForce;          // 중력의 힘
    public float gravityDistance;       // 중력의 거리

    float lookAngle;                    // 각도

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 상자와 행성과의 거리
        float dist = Vector3.Distance(gameObject.transform.position, planet.transform.position);

        // 행성에서 상자까지의 방향
        Vector3 v = planet.transform.position - transform.position;
        
        // 상자에게 힘을 가하는 것이다.
        // 거리에 따른 중력의 힘을 가한다.
        rb.AddForce(v.normalized * (1.0f - dist / gravityDistance) * gravityForce);

        // 상자를 행성의 방향으로 회전하는 것이다.
        // 방향을 이용해 회전값을 구한는 것
        lookAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
}
