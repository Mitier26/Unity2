using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 메니져.
    // 점수, 최고 점수, 게임실행 여부
    public static GameManager instance;

    [Header("Base")]
    public bool isPlay = false;
    // 딕서너리 형태로 점수를 저장하면 스테이지의 이름(씬 이름)을 가지고 점수를 이용할 수 있다.
    private Dictionary<SceneName, float> stageScoreDict = new Dictionary<SceneName, float>();
    private SceneName selectedStage;

    // 점수를 저장할 때 레벨에 따라 다른 이름으로 저장을 해야 한다.

    private void Awake()
    {
        // 싱글톤을 만든다.
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

    // 스테이지 씬에서 어떤 스테이지를 선택했는지 저장한다.
    // 씬에 따라 다른 동작을 하기 위한것이다.
    public void SetStage(SceneName stageName)
    {
        selectedStage = stageName;
        isPlay = true;
        UIManager.instance.SetGamePanel(true);
    }

    // 선택한 씬에 따라 다른 점수를 사용하기 위한것이다.
    // 게임메니져가 사라지지 않는다 점수를 0으로 초기화 하는 것이 필요하다.
    public void SetScore(float score)
    {
        // 딕서너리에 해당 키의 값이 있는지 확인한다.
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

        // UI에 표시한다.
        UIManager.instance.SetScore(stageScoreDict[selectedStage]);
    }

    public float GetScore()
    {
        return stageScoreDict[selectedStage];
    }

    public float GetHighScore()
    {
        // 최고 점수를 저장하고 최고 점수를 불러 오는 것이 필요하다.

        // 각 레벨에 맞는 최고 점수를 가지고 와야한다.
        string saveString = selectedStage.ToString() + "HighScore";

        // 각 레벨의 최고 점수를 따로 저장한다.
        float highScore = PlayerPrefs.GetFloat(saveString);

        // 현재의 점수를 가지고 온다.
        float currentScore = stageScoreDict[selectedStage];

        // 저장되어 있는 점수가 있다면
        if (PlayerPrefs.HasKey(saveString))
        {
            // 저장되어 있는 최고 점수와 현재의 점수을 비교한다.
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetFloat(saveString, highScore);
            }
        }
        else
        {
            // 저장되어 있는 점수가 없다면 현재의 점수를 저장한다.
            PlayerPrefs.SetFloat(saveString, currentScore);
            highScore = currentScore;
        }
        
        return highScore;
    }

}
