using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProShooter : MonoBehaviour
{
    public ProPlayer player;

    public Vector3 mousePos;            // 마우스의 위치
    public Vector3 direction;           // 총알의 발사 방향

    public float speed;                 // 총알의 속도
    public float shootDelay;            // 총알 발사 간격
    public float elapsedTime;           // 총알 발사 시간

    public float power;                 // 총알의 데미지

    public PoolManager poolManager;     // 오브젝트 풀

    private void Awake()
    {
        player = transform.parent.parent.GetComponent<ProPlayer>();
    }

    private void Update()
    {
        // 총알의 연속 발사를 막기 위해 총알 발사 마다 간격을 준다.
        if(elapsedTime > shootDelay)
        {
            if (Input.GetMouseButton(0))
            {
                // 총알을 발사하면 경과 시간을 초기화
                elapsedTime = 0;

                // 총알이 발사되는 방향을 정한다.
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = mousePos - transform.position;
                direction.z = 0f;
                direction.Normalize();

                // 발사 하려는 총알을 오브젝트 풀에서 가지고 온다.
                ProProjectile go = poolManager.GetFromPool<ProProjectile>(player.level);
                go.poolManager = poolManager;

                // 생성된거나 풀에서 가지고온 총알의 위치를 초기화 하고 이동 시킨다.
                go.transform.position = transform.position;
                go.tag = "PlayerBullet";
                go.GetComponent<Rigidbody2D>().velocity = direction * speed;
                go.power = power;
            }
        }
        else if(elapsedTime < shootDelay)
        {
            // 발사를 하지 않을 때 경과 시간이 계속 해서 증가하는 것을 방지한다.
            elapsedTime += Time.deltaTime;
        }
    }
}
