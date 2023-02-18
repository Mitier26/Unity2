using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;                        // 스테이지 데이터를 가지고 오기 위한것
    private Movement2D movement2D;                      // 이동하기

    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;      // 공격키 설정
    private Weapon weapon;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon= GetComponent<Weapon>();
    }

    private void Update()
    {
        // 이동하려고 하는 방향을 지정해준다.
        // GetAxisRaw를 이용해 상하좌우를 받는다.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // movement2D에 방향값을 전해 준다.
        movement2D.MoveTo(new Vector3(x, y, 0));

        // 버튼이 눌러지면 공격
        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if(Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }

    }

    private void LateUpdate()
    {
        // 플레이어가 화면 밖으로 나가지 못하기 위한 것
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
            Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y), 0);
    }
}
