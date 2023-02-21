using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool isPlayer, hasGameFinished;     // 플레이어 확인과 게임 종료확인
    // 플레이어 확인의 경우 O,X 표시 변경 용이다.

    [SerializeField]
    TextMeshProUGUI message;        // 게임의 상태, 누구의 순서인지, 누가 이겼는지 표시

    Board myBoard;                  // 게임 판

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isPlayer = true;
        hasGameFinished = false;
        myBoard = new Board();
    }
    public void GameStart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void Update()
    {
        // 마우스 버튼을 클릭하면 작동한다.
        if(Input.GetMouseButtonDown(0))
        {
            ChangText();
        }
    }

    private void ChangText()
    {
        // 마우스의 좌표를 구하는 것
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // 마우스의 좌표에서 레이저를 발사해 충돌을 감지 하기 위한 것
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if(hit.collider != null && !hit.collider.GetComponent<Click>().hasPlayed && !hasGameFinished)
        {
            hit.collider.gameObject.GetComponent<Click>().SetStore(isPlayer);

            if (hasGameFinished) return;

            if(isPlayer)
            {
                message.text = "O turn";
            }
            else
            {
                message.text = "X turn";
            }
            isPlayer = !isPlayer;
        }
    }

    // 결과 출력용
    public void DetermineWinner(int row, int col, bool isPlayer)
    {
        // 클릭하고 X,O를 표시한 후
        // UpdateCell 에서 검사를 한다.
        
        if(myBoard.UpdateCell(row, col, isPlayer))
        {
            hasGameFinished = true;
            message.text = isPlayer ? "X Win" : "O Win";
        }
    }
}
