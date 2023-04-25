using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� �޴���.
    // ����, �ְ� ����, ���ӽ��� ����
    public static GameManager instance;

    [Header("Base")]
    public bool isPlay = false;
    // �񼭳ʸ� ���·� ������ �����ϸ� ���������� �̸�(�� �̸�)�� ������ ������ �̿��� �� �ִ�.
    private Dictionary<SceneName, float> stageScoreDict = new Dictionary<SceneName, float>();
    private SceneName selectedStage;

    // ������ ������ �� ������ ���� �ٸ� �̸����� ������ �ؾ� �Ѵ�.

    private void Awake()
    {
        // �̱����� �����.
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �������� ������ � ���������� �����ߴ��� �����Ѵ�.
    // ���� ���� �ٸ� ������ �ϱ� ���Ѱ��̴�.
    public void SetStage(SceneName stageName)
    {
        selectedStage = stageName;
        isPlay = true;
        UIManager.instance.SetGamePanel(true);
    }

    // ������ ���� ���� �ٸ� ������ ����ϱ� ���Ѱ��̴�.
    // ���Ӹ޴����� ������� �ʴ´� ������ 0���� �ʱ�ȭ �ϴ� ���� �ʿ��ϴ�.
    public void SetScore(float score)
    {
        // �񼭳ʸ��� �ش� Ű�� ���� �ִ��� Ȯ���Ѵ�.
        if (stageScoreDict.ContainsKey(selectedStage))
        {
            stageScoreDict[selectedStage] += score;

            if (stageScoreDict[selectedStage] < 0)
            {
                stageScoreDict[selectedStage] = 0;
            }
        }
        else
        {
            stageScoreDict.Add(selectedStage, score);
        }

        // UI�� ǥ���Ѵ�.
        UIManager.instance.SetScore(stageScoreDict[selectedStage]);
    }

    public float GetScore()
    {
        return stageScoreDict[selectedStage];
    }

    public float GetHighScore()
    {
        // �ְ� ������ �����ϰ� �ְ� ������ �ҷ� ���� ���� �ʿ��ϴ�.

        // �� ������ �´� �ְ� ������ ������ �;��Ѵ�.
        string saveString = selectedStage.ToString() + "HighScore";

        // �� ������ �ְ� ������ ���� �����Ѵ�.
        float highScore = PlayerPrefs.GetFloat(saveString);

        // ������ ������ ������ �´�.
        float currentScore = stageScoreDict[selectedStage];

        // ����Ǿ� �ִ� ������ �ִٸ�
        if (PlayerPrefs.HasKey(saveString))
        {
            // ����Ǿ� �ִ� �ְ� ������ ������ ������ ���Ѵ�.
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetFloat(saveString, highScore);
            }
        }
        else
        {
            // ����Ǿ� �ִ� ������ ���ٸ� ������ ������ �����Ѵ�.
            PlayerPrefs.SetFloat(saveString, currentScore);
            highScore = currentScore;
        }
        
        return highScore;
    }

}
