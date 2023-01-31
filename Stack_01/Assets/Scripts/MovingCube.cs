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
        // �ش� ������ ��ȯ�⿡�� �˷��ش�.
        this.cubeSpawner = cubeSpawner;
        this.perfectController = perfectController;
        this.moveAxis = moveAxis;

        // ������ �����Ѵ�.
        if (moveAxis == MoveAxis.x) moveDirection = Vector3.left;
        else if (moveAxis == MoveAxis.z) moveDirection = Vector3.back;
    }

    private void Update()
    {
        // ť���� �̵�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ť�갡 �պ� �̵��ϱ� ����
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

    // �غ� �ܰ�
    public bool Arrangement()
    {
        // ť���� �̵��� �����Ѵ�.
        moveSpeed = 0;
        // ť���� ������ �ִٴ� ���� ���⿡ �����ϴ� ���� �ִٴ� ���̴�.


        // �̵��� ���� ť���� ��ġ�� ������ ť���� ��ġ�� ���Ͽ�
        // Ƣ��� �κ��� �߶󳻾� �Ѵ�.
        float hangOver = GetHangOver();

        if(IsGameOver(hangOver))
        {
            return true;
        }

        bool isPerfect = perfectController.IsPerfect(hangOver);

        if(isPerfect == false)
        {
            // ���� ť���� ������ ����Ʈ�� �ƴ� ��
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


        // ��ȯ���� ������ ť�긦 ������ ť��� �����Ѵ�.
        cubeSpawner.LastCube = this.transform;

        return false;
        // ť�갡 �̵����� �ʴ´�.
        // �߰� ����
        // ���� ť�긦 ��ȯ�ص� �ȴ�.
    }

    private float GetHangOver()
    {
        // ��� �Ÿ��� ���ϴ� ��
        // ���� �̵� ���� ���� ť��� ������ ť���� ��ġ�� ���Ѵ�.
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
        // ���ο� �߽����� ��ġ
        // �߽��� ������ ��
        float newPosition = transform.position.x - (hangOver / 2);
        // ���ο� ũ��
        // hangOver�� ( - ) �� ��� ���� Ŀ���� Abs ( ���밪 ) ���
        float newSize = transform.localScale.x - Mathf.Abs(hangOver);

        // �߸��� ��ŭ �߽� �̵�, ũ�� ����
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);

        // �߸��� ��ŭ �߸� ť�� ����
        float cubeEdge = transform.position.x + (transform.localScale.x / 2 * direction);
        float fallingBlockSize = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangOver, float direction)
    {
        // ���ο� �߽����� ��ġ
        // �߽��� ������ ��
        float newPosition = transform.position.z - (hangOver / 2);
        // ���ο� ũ��
        // hangOver�� ( - ) �� ��� ���� Ŀ���� Abs ( ���밪 ) ���
        float newSize = transform.localScale.z - Mathf.Abs(hangOver);

        // �߸��� ��ŭ �߽� �̵�, ũ�� ����
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);

        // �߸��� ��ŭ �߸� ť�� ����
        float cubeEdge = transform.position.z + (transform.localScale.z / 2 * direction);
        float fallingBlockSize = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;

        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        // ���ο� ť�긦 �����Ѵ�.
        GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // Instaniate �� �ƴϴ�
        // �⺻ ������Ʈ�� �����ϴ� ��

        // ũ��� ������ ����
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

        // ������ ť���� ������ �̵��ߴ� ť���� �������� ����
        clone.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        // ������ ť�꿡 Rigidbody�� �߰��Ͽ� �������� �����.
        clone.AddComponent<Rigidbody>();

        Destroy(clone, 2f);
    }

    private bool IsGameOver(float hangOver)
    {
        float max = moveAxis == MoveAxis.x ? cubeSpawner.LastCube.transform.localScale.x : cubeSpawner.LastCube.transform.localScale.z;

        // ��ġ�� �ʴ� �κ��� ������ ť�� ���� ũ��
        if(Mathf.Abs(hangOver) > max)
        {
            // ���� ����
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
