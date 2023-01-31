using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성된 큐브의 이동 방향을 정의 하기 위한 것
public enum MoveAxis { x = 0, z}
public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] cubeSpawnPoints;                    // 큐브의 생성 위치
    [SerializeField]
    private Transform movingCubePrefab;                     // 생성할 큐브 프리팹

    [SerializeField]
    private PerfectController perfectController;            // 퍼펙트 이펙트 생성을 위한



    // 새로운 큐브 생성에 필요한 위치/크기, 조각 큐브, 게임오버 감시 등에 사용한다.
    [field: SerializeField] // 프로퍼티를 인스펙터에서 보이게 하기 위해 사용
    public Transform LastCube { get; set; }                 // 마지막에 생성한 큐브
    public MovingCube CurrentCube { get; set; } = null;     // 현재 이동 중인 큐브

    [SerializeField]
    private float colorWeight = 15.0f;                      // 색상의 비슷한 정도 ( 값이 작을 수록 비슷한 색 )
    // 완전히 새로운 색상으로 변경하기 위한 현재 횟수, 최대 횟수
    private int currentColorNumberOfTime = 5;      
    private int maxColorNumberOfTime = 5;

    private MoveAxis moveAxis = MoveAxis.x;                 // 현재 이동 축, 

    public void SpawnCube()
    {
        // 이동할 큐브를 생성
        Transform clone = Instantiate(movingCubePrefab);

        // 게임을 시작하고 첫번째 생성 큐브
        if (LastCube == null || LastCube.name.Equals("StartCubeTop"))
        {
            // 처음에는 기본 위치, 초기화한 위치에서 생성된다.
            clone.position = cubeSpawnPoints[(int)moveAxis].position;
            // 처음 생성 위치는 소환장치의 기본값을 사용하는데
            // 여기의 y 값은 1.05 이다. 그래서 처음 만들어지는 큐브의 y 값은 1.05 이다.
        }
        else
        {
            // 처음 만들어지는 큐브는 기본크기 이지만
            // 다음 부터 만들어 지는 큐브는 잘려나간 크기의 큐브를 만들어야 한다.

            //float x = cubeSpawnPoints[(int)moveAxis].position.x;
            //float z = cubeSpawnPoints[(int)moveAxis].position.z;

            float x = moveAxis == MoveAxis.x ? cubeSpawnPoints[(int)moveAxis].position.x : LastCube.position.x;
            float z = moveAxis == MoveAxis.z ? cubeSpawnPoints[(int)moveAxis].position.z : LastCube.position.z;
            // 큐브가 잘려서 작아지 경우 중심의 위치가 변한다.
            // 이동하는 방향과 같은 축은 기본위치 아니면 마지막 위치

            // 생성되는 큐브의 높이는 마지막 큐브 높이 + 생성되는 큐브 높이
            float y = LastCube.position.y + movingCubePrefab.localScale.y;

            clone.position = new Vector3(x, y, z);
        }

        // 생성되는 큐브의 크기를 변경한다.
        clone.localScale = new Vector3(LastCube.localScale.x, movingCubePrefab.localScale.y, LastCube.localScale.z);

        // 이동할 큐브 색상
        clone.GetComponent<MeshRenderer>().material.color = GetRandomColor();

        // 생성한 큐브에 소환기에서 해당 정보를 전달 한다.
        clone.GetComponent<MovingCube>().Setup(this, perfectController, moveAxis);

        // 최대값을 나눈 나머지 연산을 이용해 최대 값에서 벗어나지 않게 한다.
        moveAxis = (MoveAxis)(((int)moveAxis + 1) % cubeSpawnPoints.Length);
        // 소환 할 때 마다 1 씩 증가

        // 생성한 큐브는 정보를 마지막 큐브 정보에 저장
        // LastCube = clone;

        // LastCube는 소환된 큐브가 지정한다.

        // 소환기가 생성한 큐브를 지금 사용 중인 큐브에 저장한다.
        CurrentCube = clone.GetComponent<MovingCube>();
    }

    // 큐브의 생성 위치를 보기 위해
    private void OnDrawGizmos()
    {
        for(int i = 0; i < cubeSpawnPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cubeSpawnPoints[i].transform.position, movingCubePrefab.localScale);
        }
    }

    private Color GetRandomColor()
    {
        Color color = Color.white;

        // currentColorNumberOfTime은 5부터 시작 된다.
        // 5번은 weight 값에 의해 비슷한 색으로 변한다.
        if(currentColorNumberOfTime > 0)
        {
            float colorAmount = (1.0f / 255.0f) * colorWeight;
            // 에디터에서는 색상값이 0 ~ 255 이다. 하지만 코드에서는 0 ~ 1 사이의 값이다.
            // (1f / 255f) 로 1 사이의 값으로 변화한다.
            color = LastCube.GetComponent<MeshRenderer>().material.color;
            // 마지막 큐브의 색상. 바로 전에 만들어진 큐브의 색상에서 뺀다
            color = new Color(color.r - colorAmount, color.g - colorAmount, color.b - colorAmount);

            currentColorNumberOfTime--;
        }
        else
        {
            color = new Color(Random.value, Random.value, Random.value);

            currentColorNumberOfTime = maxColorNumberOfTime;
        }

        return color;
    }
}
