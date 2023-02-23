using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board myBoard;

    private bool hasGameFinished, isFirstTurn;

    Card first;

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

        myBoard = new Board();
        hasGameFinished = false;
        isFirstTurn = true;

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (hasGameFinished) return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null) return;

            if(hit.collider.CompareTag("Card"))
            {
                if (hit.collider.gameObject.GetComponent<Card>().hasTurnFinished) return;

                if(isFirstTurn)
                {
                    first = hit.collider.gameObject.GetComponent<Card>();
                    first.UpdateTurn();
                }
                else
                {
                    Card second = hit.collider.gameObject.GetComponent<Card>();
                    if(second.hasClicked)
                    {
                        first.RemoveTurn();
                        second.RemoveTurn();
                        isFirstTurn = !isFirstTurn;
                        return;
                    }
                    second.UpdateTurn();

                    if(first.dino == second.dino)
                    {
                        first.hasTurnFinished = true;
                        second.hasTurnFinished = true;

                        if(myBoard.UpdateChoice())
                        {
                            hasGameFinished = true;
                            return;
                        }
                        isFirstTurn = !isFirstTurn;
                        return;
                    }

                    first.RemoveTurn();
                    second.RemoveTurn();
                }
                isFirstTurn = !isFirstTurn;
            }
            
        }
    }

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
}
