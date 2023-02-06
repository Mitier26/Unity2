using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern04 : MonoBehaviour
{
    [SerializeField]
    private MovementTransform2D boss;
    [SerializeField]
    private GameObject bossProjectile;
    [SerializeField]
    private float attackRate = 1;
    [SerializeField]
    private int maxFireCount = 5;


    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boss.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        // ������ �Ʒ��� �̵�
        yield return StartCoroutine(nameof(MoveDown));

        // ���� �¿� �̵�
        StartCoroutine(nameof(MoveLeftAndRight));

        // ���� ����
        int count = 0;

        while (count < maxFireCount)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }

        // ���� ������Ʈ ��Ȱ��ȭ
        boss.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        // ��ǥ��ġ
        float bossDestivationY = 2;

        boss.gameObject.SetActive(true);

        while(true)
        {
            if(boss.transform.position.y <= bossDestivationY)
            {
                boss.MoveTo(Vector2.zero);

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator MoveLeftAndRight()
    {
        // ���� ������Ʈ�� �� �ٱ����� �̵����� �ʵ��� �ϴ� ����ġ ��
        float xWeight = 3;

        // ������ ó�� �̵��ϴ� ����
        boss.MoveTo(Vector3.right);

        while(true)
        {
            if(boss.transform.position.x <= Constants.min.x + xWeight)
            {
                boss.MoveTo(Vector3.right);
            }
            else if(boss.transform.position.x >= Constants.max.x - xWeight)
            {
                boss.MoveTo(Vector3.left);
            }

            yield return null;
        }
    }

    private void CircleFire()
    {
        int count = 30;                         // �Ѿ��� ��
        float intervalAngel = 360 / count;      // �Ѿ��� �߻� ���� ����

        for(int i = 0; i < count; i++)
        {
            // �Ѿ� ����
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            // �Ѿ��� ����
            float angle = intervalAngel * i;

            // ������ �̿��� �̵�����
            float x = Mathf.Cos(angle * Mathf.PI / 180f);
            float y = Mathf.Sin(angle * Mathf.PI / 180f);

            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
