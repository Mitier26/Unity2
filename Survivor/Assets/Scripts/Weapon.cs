using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }


    public void Init(ItemData data)
    {
        // �⺻ ����
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;        // �÷��̾ �θ�� �Ѵ�.
        transform.localPosition = Vector3.zero;     // �÷��̾ �߽����� ��ġ�� �ʱ�ȭ�Ѵ�.

        // ������Ƽ ����
        // ���Ӱ� ������� ������ ���� �ʱ�ȭ�Ѵ�.
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i = 0; i < GameManager.Instance.pool.prefabs.Length; i++)
        {
            if(data.projectile == GameManager.Instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        // �÷��̾� �ڽĿ� �ִ� ��� ������Ʈ�߿�
        // ApplyGear �޼��尡 �ִ� ��� ���� �����Ѵ�.
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if( timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage , int count)
    {
        this.damage = damage;
        this.count += count;

        if(id==0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private void Batch()
    {
        for ( int i= 0; i < count; i++)
        {
            Transform bullet;

            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);     // -1 is Infinity
        }

    }

    private void Fire()
    {
        if (player.scanner.nearestTarget == null) return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(2).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
