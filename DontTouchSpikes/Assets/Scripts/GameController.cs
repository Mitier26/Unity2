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
        // �ܺο��� ����ϱ� ���� ��
        UpdateSpikes();

        currentScore++;
        uiController.UpdateScore(currentScore);

        randomColor.OnChange();
    }

    private void UpdateSpikes()
    {
        // �ش� ��ȣ�� �ش��ϴ� ���⿡ �ִ� ���ø� Ȱ��ȭ �Ѵ�.
        spikeSpawners[currentSpawn].ActivateAll();

        // ���� Ȱ��ȭ ��ȣ�� 1�� ���� �ϰ� �ְ��� ���� �ʰ� �Ѵ�.
        currentSpawn = (currentSpawn + 1) % spikeSpawners.Length;

        // ������ 1�� ���߱� ������ �ݴ��� ���ô� ��Ȱ��ȭ �Ѵ�.
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
