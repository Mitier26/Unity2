using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ť���� �̵� ������ ���� �ϱ� ���� ��
public enum MoveAxis { x = 0, z}
public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] cubeSpawnPoints;                    // ť���� ���� ��ġ
    [SerializeField]
    private Transform movingCubePrefab;                     // ������ ť�� ������

    [SerializeField]
    private PerfectController perfectController;            // ����Ʈ ����Ʈ ������ ����



    // ���ο� ť�� ������ �ʿ��� ��ġ/ũ��, ���� ť��, ���ӿ��� ���� � ����Ѵ�.
    [field: SerializeField] // ������Ƽ�� �ν����Ϳ��� ���̰� �ϱ� ���� ���
    public Transform LastCube { get; set; }                 // �������� ������ ť��
    public MovingCube CurrentCube { get; set; } = null;     // ���� �̵� ���� ť��

    [SerializeField]
    private float colorWeight = 15.0f;                      // ������ ����� ���� ( ���� ���� ���� ����� �� )
    // ������ ���ο� �������� �����ϱ� ���� ���� Ƚ��, �ִ� Ƚ��
    private int currentColorNumberOfTime = 5;      
    private int maxColorNumberOfTime = 5;

    private MoveAxis moveAxis = MoveAxis.x;                 // ���� �̵� ��, 

    public void SpawnCube()
    {
        // �̵��� ť�긦 ����
        Transform clone = Instantiate(movingCubePrefab);

        // ������ �����ϰ� ù��° ���� ť��
        if (LastCube == null || LastCube.name.Equals("StartCubeTop"))
        {
            // ó������ �⺻ ��ġ, �ʱ�ȭ�� ��ġ���� �����ȴ�.
            clone.position = cubeSpawnPoints[(int)moveAxis].position;
            // ó�� ���� ��ġ�� ��ȯ��ġ�� �⺻���� ����ϴµ�
            // ������ y ���� 1.05 �̴�. �׷��� ó�� ��������� ť���� y ���� 1.05 �̴�.
        }
        else
        {
            // ó�� ��������� ť��� �⺻ũ�� ������
            // ���� ���� ����� ���� ť��� �߷����� ũ���� ť�긦 ������ �Ѵ�.

            //float x = cubeSpawnPoints[(int)moveAxis].position.x;
            //float z = cubeSpawnPoints[(int)moveAxis].position.z;

            float x = moveAxis == MoveAxis.x ? cubeSpawnPoints[(int)moveAxis].position.x : LastCube.position.x;
            float z = moveAxis == MoveAxis.z ? cubeSpawnPoints[(int)moveAxis].position.z : LastCube.position.z;
            // ť�갡 �߷��� �۾��� ��� �߽��� ��ġ�� ���Ѵ�.
            // �̵��ϴ� ����� ���� ���� �⺻��ġ �ƴϸ� ������ ��ġ

            // �����Ǵ� ť���� ���̴� ������ ť�� ���� + �����Ǵ� ť�� ����
            float y = LastCube.position.y + movingCubePrefab.localScale.y;

            clone.position = new Vector3(x, y, z);
        }

        // �����Ǵ� ť���� ũ�⸦ �����Ѵ�.
        clone.localScale = new Vector3(LastCube.localScale.x, movingCubePrefab.localScale.y, LastCube.localScale.z);

        // �̵��� ť�� ����
        clone.GetComponent<MeshRenderer>().material.color = GetRandomColor();

        // ������ ť�꿡 ��ȯ�⿡�� �ش� ������ ���� �Ѵ�.
        clone.GetComponent<MovingCube>().Setup(this, perfectController, moveAxis);

        // �ִ밪�� ���� ������ ������ �̿��� �ִ� ������ ����� �ʰ� �Ѵ�.
        moveAxis = (MoveAxis)(((int)moveAxis + 1) % cubeSpawnPoints.Length);
        // ��ȯ �� �� ���� 1 �� ����

        // ������ ť��� ������ ������ ť�� ������ ����
        // LastCube = clone;

        // LastCube�� ��ȯ�� ť�갡 �����Ѵ�.

        // ��ȯ�Ⱑ ������ ť�긦 ���� ��� ���� ť�꿡 �����Ѵ�.
        CurrentCube = clone.GetComponent<MovingCube>();
    }

    // ť���� ���� ��ġ�� ���� ����
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

        // currentColorNumberOfTime�� 5���� ���� �ȴ�.
        // 5���� weight ���� ���� ����� ������ ���Ѵ�.
        if(currentColorNumberOfTime > 0)
        {
            float colorAmount = (1.0f / 255.0f) * colorWeight;
            // �����Ϳ����� ������ 0 ~ 255 �̴�. ������ �ڵ忡���� 0 ~ 1 ������ ���̴�.
            // (1f / 255f) �� 1 ������ ������ ��ȭ�Ѵ�.
            color = LastCube.GetComponent<MeshRenderer>().material.color;
            // ������ ť���� ����. �ٷ� ���� ������� ť���� ���󿡼� ����
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
