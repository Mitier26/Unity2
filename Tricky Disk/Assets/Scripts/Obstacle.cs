using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPosLeft, startPosRight;   // ����, ������ ���� ��

    [SerializeField] float moveTime;                                // �̵��ϴ� �ð�

    private void Start()
    {
        // ������Ʈ�� �����Ǹ� ������ �̵��Ͽ� ����, �������� �ϳ��� �����Ѵ�.
        transform.position = Random.Range(0,1) > 0.5f ? startPosLeft : startPosRight;
    }

    // �޴������� ��ȯ�� �� �� �۵��ϰ� �ϴ� �Լ�
    public void MoveToPos(Vector3 targetPos)
    {
        StartCoroutine(IMoveToPos(targetPos));
    }

    // �̵��ϴ� ���
    public IEnumerator IMoveToPos(Vector3 targetPos)
    {
        float timeElapsed = 0f;
        while (timeElapsed < moveTime)
        {
            timeElapsed += Time.fixedDeltaTime;
            // ���� �������� ��ǥ���� ���� �̵��ϴ� ����̴�.
            // targerPos�� �̵��ϴ� �������� ��� �����ϸ� ��� �̵��Ѵ�.
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveTime * timeElapsed);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        transform.position = targetPos;
    }
}
