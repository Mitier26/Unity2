using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResetTrigger : MonoBehaviour
{
    public GameObject[] steps;


    private void OnTriggerEnter(Collider other)
    {
        // �ش� ������Ʈ�� ��ܰ� ���� ��� ������
        // ����� ��� �� ���� �۵����� �ʰ� ���� ��� �� ���� �۵��� �Ѵ�.
        // ���� : ������ Rigidbody�� �ְ� ��ܿ��� Rigidbody�� ����.
        GameObject First = GetFirstStep();
        GameObject Lost = GetLostStep();

        // ���� �Ʒ��ִ� ���� ���� ���� �ִ� ���� �������� ������ ���̴�.
        First.transform.position = new Vector3(0, Lost.transform.position.y + 1, Lost.transform.position.z + 2);

        if(SliderController.hasGameStarted == true)
        {
            // ��ֹ� ����
            First.GetComponent<GenerateRandomObstacles>().Generator();
        }

        // ȭ�鿡 �ִ� ��� ������Ʈ�� ã�� ��ȯ�Ѵ�.
        Transform[] allTransforms = GetAllRelevantTransforms();

        // ȭ�鿡 �ִ� ��� ���� �̵���Ű�� ���
        foreach (Transform t in allTransforms)
        {
            t.position = new Vector3(t.position.x, t.position.y - 1, t.position.z - 2);
        }
        
        // �浹���� FindGameObject�� ���� ����ȴ�.
        // �����ؾ� �Ѵ�.
    }

    private GameObject GetFirstStep()
    {
        // ���⼭ FindGameObjectsWithTag�� ����� ��� ���� ã�Ƴ���.
        // �������� �۵��� ������ ġ������ ������ �ִ�.
        // FindGameObjectsWithTag�� �ϸ� ���� �������� ���� ���� [0] �� �ε����� ����.
        // ����Ƽ ���������� �ε����� �ο��ϰ� �� ������ ���� �迭�� �ִ� ���̴�.
        // ������ �������� �ε����� Ȯ���ϰų� ������ ����� ����.
        // GetInstanceID, GetHashCode�� ����ϸ� �� �� �ִٰ� ������ 
        // Ȯ�� ��� ����� ����.
        // ������Ʈ�� 1 �� �� ����� �������� �۵��ϴ°� ó�� ��������
        // �ѹ��� ���� ������Ʈ�� ����� �ٸ��� �۵��Ѵ�.
        // ���� �Ʒ� ó�� ����� ����� ������ ���� �� �ִ�.
        // �ذ� ����� ������ ó�� ���۵� �� ����� ����� �迭�� �ִ� ����� �ְڴ�.
        steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject firstStep = steps[0];    // ���⼭�� ������Ʈ�� ������� ����� ��������� ������� �ʴ´�.
        float z = steps[0].transform.position.z;
        // ���⼭ firstStep�� ���� �Ʒ� �ִ� ���̴�.
        // firstStep�� ���� �Ʒ� ���� ��� �Ʒ��ִ� foreach�� �۵� �����ʰ� ���ϵȴ�.
        
        // ���� z ���� ���� ���� first�� �����ϴ� ���
        foreach (GameObject step in steps)
        {
            if (step.transform.position.z < z)
            {
                z = step.transform.position.z;
                firstStep = step;
            }
        }

        return firstStep;
    }

    private GameObject GetLostStep()
    {
        // ���� ���� �ִ� ����� ã�� ���
        GameObject[] steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject lostStep = steps[0];

        float z = steps[0].transform.position.z;

        foreach (GameObject step in steps)
        {
            if (step.transform.position.z > z)
            {
                z = step.transform.position.z;
                lostStep = step;
            }
        }

        return lostStep;
    }

    private Transform[] GetAllRelevantTransforms()
    {
        GameObject[] steps = GameObject.FindGameObjectsWithTag("Step");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject camera = Camera.main.gameObject;

        List<Transform> allTransform = new List<Transform>();

        foreach(GameObject step in steps)
        {
            allTransform.Add(step.transform);
        }
        allTransform.Add(player.transform);
        allTransform.Add(camera.transform);
        return allTransform.ToArray();
    }
}
