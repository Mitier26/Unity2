using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    public bool rotateToCenter = true;
    public Attractor currentAttractor;      // 달라 붙은 행성
    public float gravityStrength = 100;     // 중력의 강도

    private Transform m_transform;
    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(currentAttractor != null)
        {
            // 리스트 안에 이것이 없다면
            if(!currentAttractor.attractedObjects.Contains(m_collider))
            {
                // 행성이 없는 것이다.
                currentAttractor = null;
                return;
            }
            if (rotateToCenter) RotateToCenter();
            m_rigidbody.gravityScale = 0;
        }
        else
        {
            m_rigidbody.gravityScale = 1;
        }
    }

    // 행성에서 달라 붙는 것에게 실행하는 부분
    public void Attract(Attractor attractorObj)
    {
        // 행성과 자신과의 방향
        Vector2 attractionDir = ((Vector2)attractorObj.attractorTransform.position - m_rigidbody.position).normalized;
        // 위에서 구한 방향으로 중력을 가한다.
        m_rigidbody.AddForce(attractionDir * -attractorObj.gravity * gravityStrength * Time.fixedDeltaTime);

        if(currentAttractor == null)
        {
            currentAttractor = attractorObj;
        }
    }

    private void RotateToCenter()
    {
        if(currentAttractor != null)
        {
            Vector2 distanceVector = (Vector2)currentAttractor.attractorTransform.position - (Vector2)m_transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
}
