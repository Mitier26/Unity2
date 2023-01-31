using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpikeSpawner[] spikeSpawners;
    private int currentSpawn = 0;

    private UIController uiController;
    [SerializeField]
    private Player player;

    private int currentScore = 0;

    private RandomColor randomColor;


    private void Awake()
    {
        uiController = GetComponent<UIController>();
        randomColor = GetComponent<RandomColor>();
        
    }

    private IEnumerator Start()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                uiController.GameStart();
                player.GameStart();

                yield break;
            }

            yield return null;
        }
    }

    public void CollisionWithWall()
    {
        // 외부에서 사용하기 위한 것
        UpdateSpikes();

        currentScore++;
        uiController.UpdateScore(currentScore);

        randomColor.OnChange();
    }

    private void UpdateSpikes()
    {
        // 해당 번호에 해당하는 방향에 있는 가시를 활성화 한다.
        spikeSpawners[currentSpawn].ActivateAll();

        // 가시 활성화 번호에 1을 증가 하고 최고량을 넘지 않게 한다.
        currentSpawn = (currentSpawn + 1) % spikeSpawners.Length;

        // 위에서 1을 더했기 때문에 반대편 가시는 비활성화 한다.
        spikeSpawners[currentSpawn].DeactivateAll();
    }

    public void GameOver()
    {
        StartCoroutine(nameof(GameOverProcess));
    }

    private IEnumerator GameOverProcess()
    {
        if(currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        uiController.GameOver();

        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }

            yield return null;
        }
    }
}
