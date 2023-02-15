using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrern07 : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject doctorKO;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Collider2D[] lasercollider2D;
    [SerializeField]
    private float rotateTime;
    [SerializeField]
    private int anglePerSeconds;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        // ����, �ڻ�, ������ Ȱ��ȭ
        ground.SetActive(true);
        doctorKO.SetActive(true);
        laser.SetActive(true);

        // �������� ó�� ���� ���� �� �浹���� �ʰ� 0.5���� �浹 �ϵ��� �Ѵ�.
        for(int i = 0; i < lasercollider2D.Length; i++)
        {
            lasercollider2D[i].enabled = false;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < lasercollider2D.Length; i++)
        {
            lasercollider2D[i].enabled = true;
        }

        float time = 0;

        while(time < rotateTime)
        {
            laser.transform.Rotate(Vector3.forward * anglePerSeconds * Time.deltaTime);

            time += Time.deltaTime;

            yield return null;
        }


        ground.SetActive(false);
        doctorKO.SetActive(false);
        laser.SetActive(false);

        gameObject.SetActive(false);
    }
}