using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject red, blue;

    private bool isPlayer, hasGameFinished;

    [SerializeField]
    private TextMeshProUGUI turnMessage;

    const string RED_MESSAGE = "RED TURN";
    const string BLUE_MESSAGE = "BLUE TURN";

    Color32 RED_COLOR = Color.red;
    Color32 BLUE_COLOR = Color.blue;

    Board myBoard;

    private void Awake()
    {
        isPlayer = true;
        hasGameFinished = false;
        turnMessage.text = RED_MESSAGE;
        turnMessage.color = RED_COLOR;

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
        if(Input.GetMouseButtonDown(0)) 
        {
            if (hasGameFinished) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider) return;

            if(hit.collider.CompareTag("Press"))
            {
                if (hit.collider.gameObject.GetComponent<Column>().targetLocation.y > 1.5) return;

                Vector3 spawnPos = hit.collider.gameObject.GetComponent<Column>().spawnLocation;
                Vector3 targetPos = hit.collider.gameObject.GetComponent<Column>().targetLocation;
                GameObject circle = Instantiate(isPlayer ? red : blue);
                circle.transform.position = spawnPos;
                circle.GetComponent<Mover>().targetPosition = targetPos;

                hit.collider.gameObject.GetComponent<Column>().targetLocation = new Vector3(targetPos.x, targetPos.y + 0.7f, targetPos.z);

                // board
                myBoard.UpdateBoard(hit.collider.gameObject.GetComponent<Column>().col - 1, isPlayer);

                if(myBoard.Result(isPlayer))
                {
                    turnMessage.text = (isPlayer ? "RED" : "BLUE") + " Wins";
                    hasGameFinished = true;
                    return;
                }

                turnMessage.text = !isPlayer ? RED_MESSAGE : BLUE_MESSAGE;
                turnMessage.color = !isPlayer ? RED_COLOR : BLUE_COLOR;

                isPlayer = !isPlayer;
            }
            
        }
    }
}
