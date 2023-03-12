using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputNumber;             // ���ڸ� �Է��ϴ� ��
    [SerializeField]
    private Image imageNumber;                      // 
    [SerializeField]
    private TextMeshProUGUI textResult;             // �ֻ����� ��� ���

    private string result;                          // ��� �ؽ�Ʈ
    private int diceResult;                         // �������� ������ ������ ���� ����

    private int totalCount = 0;                     // �ֻ��� ���� Ƚ��
    private int totalSixCount = 0;                  // 6�� ���� Ƚ��

    public void OnClickRoll()
    {
        // �ʵ��� ����� �Է� ������ �ٲ�� ���� �� ������ �ʱ�ȭ
        OnMessage(Color.white, string.Empty);

        // �Էµ� ���� ������ ����
        if (inputNumber.text.Trim().Equals(""))
        {
            OnMessage(Color.red, "Please Fill Number");
            return;
        }

        // �Էµ� ���ڸ� ���ڷ� ��ȯ
        int parse = int.Parse(inputNumber.text);

        // �Էµ� ���ڰ� 1 ~ 6 ������ ���� �ƴϸ� ����
        if(parse < 1 || parse > 6)
        {
            OnMessage(Color.red, "Please enter between 1 to 6");
        }

        // ������ �� ��ȯ
        //diceResult = BaseDice.Roll();

        // ���۵� �ֻ���
        diceResult = ControlDice.Roll();

        if(diceResult == 6)
        {
            totalSixCount++;
        }

        totalCount++;

        // �Էµ� ���� ������ ���ڰ� ������
        if(diceResult == parse)
        {
            result = $"Dice Result : {diceResult}\nYou Win!!\nTotal : {totalCount}\nTotal Six : {totalSixCount}";
        }
        else
        {
            result = $"Dice Result : {diceResult}\nYou Lose!\nTotal : {totalCount}\nTotal Six : {totalSixCount}";
        }

        textResult.text = result;
    }

    private void OnMessage(Color color, string msg)
    {
        imageNumber.color = color;
        textResult.text = msg;
    }
}
