using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputNumber;             // 숫자를 입력하는 곳
    [SerializeField]
    private Image imageNumber;                      // 
    [SerializeField]
    private TextMeshProUGUI textResult;             // 주사위의 결과 출력

    private string result;                          // 결과 텍스트
    private int diceResult;                         // 랜덤으로 생성된 임의의 숫자 저장

    private int totalCount = 0;                     // 주사위 굴린 횟수
    private int totalSixCount = 0;                  // 6이 나온 횟수

    public void OnClickRoll()
    {
        // 필드의 색상과 입력 정보가 바뀌어 있을 수 있으니 초기화
        OnMessage(Color.white, string.Empty);

        // 입력된 것이 없으면 에러
        if (inputNumber.text.Trim().Equals(""))
        {
            OnMessage(Color.red, "Please Fill Number");
            return;
        }

        // 입력된 글자를 숫자로 변환
        int parse = int.Parse(inputNumber.text);

        // 입력된 숫자가 1 ~ 6 사이의 값이 아니면 에러
        if(parse < 1 || parse > 6)
        {
            OnMessage(Color.red, "Please enter between 1 to 6");
        }

        // 임의의 값 반환
        //diceResult = BaseDice.Roll();

        // 조작된 주사위
        diceResult = ControlDice.Roll();

        if(diceResult == 6)
        {
            totalSixCount++;
        }

        totalCount++;

        // 입력된 값고 임의의 숫자가 같으면
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
