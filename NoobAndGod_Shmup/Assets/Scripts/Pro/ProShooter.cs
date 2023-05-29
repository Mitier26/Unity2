using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProShooter : MonoBehaviour
{
    public ProPlayer player;

    public Vector3 mousePos;            // ���콺�� ��ġ
    public Vector3 direction;           // �Ѿ��� �߻� ����

    public float speed;                 // �Ѿ��� �ӵ�
    public float shootDelay;            // �Ѿ� �߻� ����
    public float elapsedTime;           // �Ѿ� �߻� �ð�

    public float power;                 // �Ѿ��� ������

    public PoolManager poolManager;     // ������Ʈ Ǯ

    private void Awake()
    {
        player = transform.parent.parent.GetComponent<ProPlayer>();
    }

    private void Update()
    {
        // �Ѿ��� ���� �߻縦 ���� ���� �Ѿ� �߻� ���� ������ �ش�.
        if(elapsedTime > shootDelay)
        {
            if (Input.GetMouseButton(0))
            {
                // �Ѿ��� �߻��ϸ� ��� �ð��� �ʱ�ȭ
                elapsedTime = 0;

                // �Ѿ��� �߻�Ǵ� ������ ���Ѵ�.
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = mousePos - transform.position;
                direction.z = 0f;
                direction.Normalize();

                // �߻� �Ϸ��� �Ѿ��� ������Ʈ Ǯ���� ������ �´�.
                ProProjectile go = poolManager.GetFromPool<ProProjectile>(player.level);
                go.poolManager = poolManager;

                // �����Ȱų� Ǯ���� ������� �Ѿ��� ��ġ�� �ʱ�ȭ �ϰ� �̵� ��Ų��.
                go.transform.position = transform.position;
                go.tag = "PlayerBullet";
                go.GetComponent<Rigidbody2D>().velocity = direction * speed;
                go.power = power;
            }
        }
        else if(elapsedTime < shootDelay)
        {
            // �߻縦 ���� ���� �� ��� �ð��� ��� �ؼ� �����ϴ� ���� �����Ѵ�.
            elapsedTime += Time.deltaTime;
        }
    }
}
