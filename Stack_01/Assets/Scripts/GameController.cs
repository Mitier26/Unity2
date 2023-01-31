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
        // 게임이 시작되면 무한 반복한다.
        while(true)
        {
            // 퍼펙트 테스트
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


            // 마우스 왼쪽을 누르면 큐브를 생성한다.
            if(Input.GetMouseButtonDown(0))
            {
                if(isGameStart == false)
                {
                    isGameStart = true;
                    uiController.GameStart();
                }

                // 이동 중이 큐브가 있다면
                if(cubeSpawner.CurrentCube != null)
                {
                    // 이동 중인 큐브에 있는 Arrangement를 실행한다.
                    // cubeSpawner.CurrentCube.Arrangement();
                    // 정지는 여기서
                    // input -> cubeSpawner -> movingCube 순서로 이동한다.

                    bool isGameOver = cubeSpawner.CurrentCube.Arrangement();
                    if(isGameOver == true)
                    {
                        // 게임 오버
                        //OnGameOver();
                        // 에니메이션 작동이 끝나면 OnGameOver 실행
                        cameraController.GameOverAnimation(cubeSpawner.LastCube.position.y, OnGameOver);

                        yield break;
                    }

                    currentScore++;
                    uiController.UpdateScore(currentScore);
                }

                // 카메라를 0.1 위로 올린다.
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
