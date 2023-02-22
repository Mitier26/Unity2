using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Board myboard;
    [SerializeField]
    private Text message;

    public static GameManager instance;

    private bool hasGameFinished;

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        message.text = "Play the next Turn";
        hasGameFinished = false;
        myboard = new Board();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (hasGameFinished) return;

            // 마우스의 좌표 입력 받기
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null) return;

            if(hit.collider.CompareTag("Card"))
            {
                Card card = hit.collider.GetComponent<Card>();

                if (card.hasClicked) return;

                card.PlayTurn();

                if(card.myChoice == Choice.Gold)
                {
                    hasGameFinished = true;
                    message.text = "You Wins!";
                }
                else if(card.myChoice == Choice.Shark)
                {
                    hasGameFinished = true;
                    message.text = "You Lose....";
                }
            }
        }
    }
}
