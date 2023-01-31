using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner cubeSpawner;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private UIController uiController;

    private bool isGameStart = false;

    private int currentScore = 0;

    private IEnumerator Start()
    {
        // ������ ���۵Ǹ� ���� �ݺ��Ѵ�.
        while(true)
        {
            // ����Ʈ �׽�Ʈ
            if (Input.GetMouseButtonDown(1))
            {
                if(cubeSpawner.CurrentCube != null)
                {
                    cubeSpawner.CurrentCube.transform.position = cubeSpawner.LastCube.position + Vector3.up * 0.1f;
                    cubeSpawner.CurrentCube.Arrangement();
                    currentScore++;
                    uiController.UpdateScore(currentScore);

                }

                cameraController.MoveOneStep();
                cubeSpawner.SpawnCube();
            }


            // ���콺 ������ ������ ť�긦 �����Ѵ�.
            if(Input.GetMouseButtonDown(0))
            {
                if(isGameStart == false)
                {
                    isGameStart = true;
                    uiController.GameStart();
                }

                // �̵� ���� ť�갡 �ִٸ�
                if(cubeSpawner.CurrentCube != null)
                {
                    // �̵� ���� ť�꿡 �ִ� Arrangement�� �����Ѵ�.
                    // cubeSpawner.CurrentCube.Arrangement();
                    // ������ ���⼭
                    // input -> cubeSpawner -> movingCube ������ �̵��Ѵ�.

                    bool isGameOver = cubeSpawner.CurrentCube.Arrangement();
                    if(isGameOver == true)
                    {
                        // ���� ����
                        //OnGameOver();
                        // ���ϸ��̼� �۵��� ������ OnGameOver ����
                        cameraController.GameOverAnimation(cubeSpawner.LastCube.position.y, OnGameOver);

                        yield break;
                    }

                    currentScore++;
                    uiController.UpdateScore(currentScore);
                }

                // ī�޶� 0.1 ���� �ø���.
                cameraController.MoveOneStep();

                cubeSpawner.SpawnCube();
            }

            yield return null;
        }
    }

    private void OnGameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if(currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            uiController.GameOver(true);
        }
        else
        {
            uiController.GameOver(false);
        }

        StartCoroutine("AfterGameOver");
    }

    private IEnumerator AfterGameOver()
    {
        yield return new WaitForEndOfFrame();

        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            yield return null;
        }
    }
}
