using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern02 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages;
    [SerializeField]
    private GameObject[] playerObject;
    [SerializeField]
    private float spawnCycle = 1;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        for(int i = 0; i < playerObject.Length; i++)
        {
            playerObject[i].SetActive(false);
            playerObject[i].GetComponent<MovingEntity>().Reset();
        }

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        int[] numbers = Utils.RandomNumbers(3, 3);

        int index = 0;

        while(index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnPlayer), numbers[index]);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPlayer(int index)
    {
        // ��� �̹��� Ȱ��/ ��Ȱ��
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        // �÷��̾� ������Ʈ Ȱ��ȭ
        playerObject[index].SetActive(true);
    }
}
