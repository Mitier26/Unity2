using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool isPlayer, hasGameFinished;     // �÷��̾� Ȯ�ΰ� ���� ����Ȯ��
    // �÷��̾� Ȯ���� ��� O,X ǥ�� ���� ���̴�.

    [SerializeField]
    TextMeshProUGUI message;        // ������ ����, ������ ��������, ���� �̰���� ǥ��

    Board myBoard;                  // ���� ��

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
        // ���콺 ��ư�� Ŭ���ϸ� �۵��Ѵ�.
        if(Input.GetMouseButtonDown(0))
        {
            ChangText();
        }
    }

    private void ChangText()
    {
        // ���콺�� ��ǥ�� ���ϴ� ��
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // ���콺�� ��ǥ���� �������� �߻��� �浹�� ���� �ϱ� ���� ��
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

    // ��� ��¿�
    public void DetermineWinner(int row, int col, bool isPlayer)
    {
        // Ŭ���ϰ� X,O�� ǥ���� ��
        // UpdateCell ���� �˻縦 �Ѵ�.
        
        if(myBoard.UpdateCell(row, col, isPlayer))
        {
            hasGameFinished = true;
            message.text = isPlayer ? "X Win" : "O Win";
        }
    }
}
