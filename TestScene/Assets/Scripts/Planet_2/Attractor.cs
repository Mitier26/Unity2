using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask attractionLayer;       // �߷¿� ������ �� ���̾�
    public float gravity = 10;              // �߷��� ��
    public float radius = 10;               // �߷��� ����
    public List<Collider2D> attractedObjects = new List<Collider2D>();
    public Transform attractorTransform;    // �ڱ� �ڽ�

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
        // �ڱ� �ڽſ��� radius ��ŭ �� ���� ���� �ִ� ���� ã�Ƽ� ����Ʈ�� �ִ´�.
        attractedObjects = Physics2D.OverlapCircleAll(attractorTransform.position, radius, attractionLayer).ToList();
    }

    private void AttractObjects()
    {
        // ����Ʈ�� �ִ� �͵��� ��ȸ�Ͽ� Attrack �� �����Ѵ�.
        // �޶� �ٴ� �͵��� Attactable �� �־�� �Ѵ�.
        for(int i = 0; i < attractedObjects.Count; i++)
        {
            attractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ������ ������Ʈ�� ������ ���� ���̰� �����.
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
