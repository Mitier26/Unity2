using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlayer : MonoBehaviour
{
    RaycastHit2D hit;
    public float detectDistance = 1f;
    public float testRot = 0;
    public Vector2 normal;
    public Vector2 hitPos;
    public Vector2 rePos;
    public float angle;
    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, detectDistance, LayerMask.GetMask("Ground"));
        //hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.down, detectDistance, LayerMask.GetMask("Ground"));

        if (hit)
        {
            // 법선 벡터를 이용하여 플레이어 회전
            Vector2 normal = hit.normal;
            float angle = Mathf.Atan2(normal.x, normal.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * 5);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * detectDistance);
        //Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.down * detectDistance);
    }
}
