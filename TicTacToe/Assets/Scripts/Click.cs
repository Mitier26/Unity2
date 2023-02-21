using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    
    // X,O �� ǥ���ϴ� �κ�
    // ��9 ���� ����� ȭ�鿡 ��ġ�ߴ�.

    [SerializeField]
    private int row, col;               // ����, ���� ��ġ�� �����Ѵ�.

    [SerializeField]
    private TextMeshProUGUI text;       // ǥ�õǴ� ����

    public bool hasPlayed;              // ������ ���� �Ǿ����� Ȯ���Ѵ�.

    private void Awake()
    {
        // �ʱ�ȭ
        hasPlayed = false;
        text.text = "";
    }

    // ���Ӹ޴������� ȭ���� Ŭ�� ���� ��
    // �������� üũ�� �����ϴٸ� �̰��� ���� �ϰ� �ȴ�.
    // isPlayer�� üũ�� �Ϸ�� �� ���� ����ȴ�.
    public void SetStore(bool isPlayer)
    {
        if(isPlayer)
        {
            text.text = "X";
            text.color = Color.red;
        }
        else
        {
            text.text = "O";
            text.color = Color.blue;
        }
        // ǥ�ø� �ϸ� ���� �޴����� �ִ� ���� �����Ѵ�.
        GameManager.instance.DetermineWinner(row, col, isPlayer);
        hasPlayed = true;
    }
}
