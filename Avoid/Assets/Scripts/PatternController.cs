using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject[] patterns;                  // �����ϰ� �ִ� ����
    [SerializeField]
    private GameObject hpPotion;
    private GameObject currentPattern;              // ���� �������� ����
    private int[] patternIndexs;                    // ��ġ�� �ʴ� patterns.Length ������ ����
    private int current = 0;                        // PatternIndexs �迭�� ����

    private void Awake()
    {
        // �����ϰ� �ִ� ���ϰ����� �����ϰ� �޸� �Ҵ�
        patternIndexs = new int[patterns.Length];

        // ó������ ������ ���������� ����
        for(int i =0; i < patternIndexs.Length; i++)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        // ���� ������� ������ ����Ǿ� ������Ʈ�� ��Ȱ��ȭ�Ǹ�
        if(currentPattern.activeSelf == false)
        {
            ChangePattern();
        }
    }

    public void GameStart()
    {
        ChangePattern();
    }

    public void GameOver()
    {
        // ���� �������� ���ϸ� ��Ȱ��Ȱ
        currentPattern.SetActive(false);
    }

    public void ChangePattern()
    {
        // ���� ���� ����
        currentPattern = patterns[patternIndexs[current]];

        // ���� ���� Ȱ��ȭ
        currentPattern.SetActive(true);

        current++;

        if(current == 4 || current == 7 || current == 10)
        {
            hpPotion.SetActive(true);
        }

        // ������ �ѹ��� ��� �����ߴٸ� ���� ������ ��ġ�� �ʴ� ������ ���ڷ� ����
        if(current >= patterns.Length)
        {
            patternIndexs = Utils.RandomNumbers(patterns.Length, patterns.Length);
            current = 0;
        }
    }
}
