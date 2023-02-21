using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    
    // X,O 를 표시하는 부분
    // 총9 개를 만들어 화면에 배치했다.

    [SerializeField]
    private int row, col;               // 가로, 세로 위치를 저장한다.

    [SerializeField]
    private TextMeshProUGUI text;       // 표시되는 문자

    public bool hasPlayed;              // 게임이 시작 되었는지 확인한다.

    private void Awake()
    {
        // 초기화
        hasPlayed = false;
        text.text = "";
    }

    // 게임메니져에서 화면을 클릭 했을 때
    // 판정으로 체크가 가능하다면 이것을 실행 하게 된다.
    // isPlayer는 체크가 완료될 때 마다 변경된다.
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
        // 표시를 하면 게임 메니져에 있는 것을 실행한다.
        GameManager.instance.DetermineWinner(row, col, isPlayer);
        hasPlayed = true;
    }
}
