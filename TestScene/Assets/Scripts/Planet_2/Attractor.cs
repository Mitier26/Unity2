using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask attractionLayer;       // 중력에 영향을 줄 레이어
    public float gravity = 10;              // 중력의 힘
    public float radius = 10;               // 중력의 범위
    public List<Collider2D> attractedObjects = new List<Collider2D>();
    public Transform attractorTransform;    // 자기 자신

    private void Awake()
    {
        attractorTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        SetAttractedObjects();
    }

    private void FixedUpdate()
    {
        AttractObjects();
    }

    private void SetAttractedObjects()
    {
        // 자기 자신에서 radius 만큼 원 범위 내에 있는 것을 찾아서 리스트에 넣는다.
        attractedObjects = Physics2D.OverlapCircleAll(attractorTransform.position, radius, attractionLayer).ToList();
    }

    private void AttractObjects()
    {
        // 리스트에 있는 것들을 순회하여 Attrack 를 실행한다.
        // 달라 붙는 것들이 Attactable 이 있어야 한다.
        for(int i = 0; i < attractedObjects.Count; i++)
        {
            attractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 선택한 오브젝트의 범위를 눈에 보이게 만든다.
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
