using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;
    private Vector3 moveDirection;

    private CubeSpawner cubeSpawner;
    private MoveAxis moveAxis;

    private PerfectController perfectController;


    public void Setup(CubeSpawner cubeSpawner, PerfectController perfectController, MoveAxis moveAxis)
    {
        // 해당 정보는 소환기에서 알려준다.
        this.cubeSpawner = cubeSpawner;
        this.perfectController = perfectController;
        this.moveAxis = moveAxis;

        // 방향을 결정한다.
        if (moveAxis == MoveAxis.x) moveDirection = Vector3.left;
        else if (moveAxis == MoveAxis.z) moveDirection = Vector3.back;
    }

    private void Update()
    {
        // 큐브의 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 큐브가 왕복 이동하기 위해
        if(moveAxis == MoveAxis.x)
        {
            if (transform.position.x <= -1.5f) moveDirection = Vector3.right;
            else if (transform.position.x >= 1.5) moveDirection = Vector3.left;
        }
        else if(moveAxis == MoveAxis.z)
        {
            if (transform.position.z <= -1.5f) moveDirection = Vector3.forward;
            else if(transform.position.z >= 1.5f) moveDirection = Vector3.back;
        }
    }

    // 준비 단계
    public bool Arrangement()
    {
        // 큐브의 이동을 정지한다.
        moveSpeed = 0;
        // 큐브의 정지가 있다는 것은 여기에 판정하는 것이 있다는 것이다.


        // 이동을 멈춘 큐브의 위치와 마지막 큐브의 위치를 비교하여
        // 튀어나온 부분을 잘라내야 한다.
        float hangOver = GetHangOver();

        if(IsGameOver(hangOver))
        {
            return true;
        }

        bool isPerfect = perfectController.IsPerfect(hangOver);

        if(isPerfect == false)
        {
            // 조각 큐브의 생성은 퍼펙트가 아닐 때
            float direction = hangOver >= 0 ? 1 : -1;

            if (moveAxis == MoveAxis.x)
            {
                SplitCubeOnX(hangOver, direction);
            }
            else if (moveAxis == MoveAxis.z)
            {
                SplitCubeOnZ(hangOver, direction);
            }

        }


        // 소환기의 마지막 큐브를 지금의 큐브로 설정한다.
        cubeSpawner.LastCube = this.transform;

        return false;
        // 큐브가 이동하지 않는다.
        // 중간 판정
        // 다음 큐브를 소환해도 된다.
    }

    private float GetHangOver()
    {
        // 벗어난 거리를 구하는 것
        // 현재 이동 중이 였던 큐브와 마지막 큐브의 위치를 비교한다.
        float amount = 0f;

        if(moveAxis == MoveAxis.x)
        {
            amount = transform.position.x - cubeSpawner.LastCube.transform.position.x;
        }
        else if(moveAxis == MoveAxis.z)
        {
            amount = transform.position.z - cubeSpawner.LastCube.transform.position.z;
        }

        return amount;
    }

    private void SplitCubeOnX(float hangOver, float direction)
    {
        // 새로운 중심점의 위치
        // 중심은 길이의 반
        float newPosition = transform.position.x - (hangOver / 2);
        // 새로운 크기
        // hangOver가 ( - ) 일 경우 값이 커지니 Abs ( 절대값 ) 사용
        float newSize = transform.localScale.x - Mathf.Abs(hangOver);

        // 잘리는 만큼 중심 이동, 크기 변경
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);

        // 잘리는 만큼 잘린 큐브 생성
        float cubeEdge = transform.position.x + (transform.localScale.x / 2 * direction);
        float fallingBlockSize = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangOver, float direction)
    {
        // 새로운 중심점의 위치
        // 중심은 길이의 반
        float newPosition = transform.position.z - (hangOver / 2);
        // 새로운 크기
        // hangOver가 ( - ) 일 경우 값이 커지니 Abs ( 절대값 ) 사용
        float newSize = transform.localScale.z - Mathf.Abs(hangOver);

        // 잘리는 만큼 중심 이동, 크기 변경
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);

        // 잘리는 만큼 잘린 큐브 생성
        float cubeEdge = transform.position.z + (transform.localScale.z / 2 * direction);
        float fallingBlockSize = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        // 새로운 큐브를 생선한다.
        GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Instaniate 가 아니다
        // 기본 오브젝트를 생성하는 것

        // 크기와 방향을 설정
        if(moveAxis == MoveAxis.x)
        {
            clone.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
            clone.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        }
        else if(moveAxis == MoveAxis.z)
        {
            clone.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
            clone.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        }

        // 생성된 큐브의 색상을 이동했던 큐브의 색상으로 변경
        clone.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        // 생성된 큐브에 Rigidbody를 추가하여 떨어지게 만든다.
        clone.AddComponent<Rigidbody>();

        Destroy(clone, 2f);
    }

    private bool IsGameOver(float hangOver)
    {
        float max = moveAxis == MoveAxis.x ? cubeSpawner.LastCube.transform.localScale.x : cubeSpawner.LastCube.transform.localScale.z;

        // 겹치지 않는 부분이 마지막 큐브 보다 크면
        if(Mathf.Abs(hangOver) > max)
        {
            // 게임 오버
            return true;
        }

        return false;
    }

    public void RecoveryCube()
    {
        float recoverySize = 0.1f;

        if(moveAxis == MoveAxis.x)
        {
            float newXSize = transform.localScale.x + recoverySize;
            float newXPosition = transform.position.x + recoverySize * 0.5f;

            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            float newZSize = transform.localScale.z + recoverySize;
            float newZPosition = transform.position.z + recoverySize * 0.5f;

            transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        }
    }
}
