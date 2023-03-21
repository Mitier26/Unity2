using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmateurManager : MonoBehaviour
{
    public static AmateurManager instance;              // 싱글톤

    public bool isLive = true;                          // 플레이어가 살아있는지 확인
    public bool isPlay = false;                         // 게임이 시작 되었는지 확인

    [SerializeField]
    private GameObject one, two, three, go;             // 카운트 다운 오브젝트

    [SerializeField]
    private AmateurSpawner spawner;                         // 소환기에 명령을 전달하기 위해 
    
    [SerializeField]
    private GameObject gameOverPanel;                   // 게임 오버 오브젝트

    [SerializeField]
    private TextMeshProUGUI scoreText;                  // 게임 내에 표시되는 점수
    [SerializeField]
    private TextMeshProUGUI resultScoreText;            // 결과 창에 표시되는 점수
    [SerializeField]    
    private TextMeshProUGUI resultHighScoreText;        // 결과 창에 표시되는 최고 점수
    [SerializeField]
    private TextMeshProUGUI levelText;                  // 레벨 표시
   

    [SerializeField]
    private float scoreTime = 1f;                       // 1점이 올라가는 시간

    private int levelupPoint = 21;                      // 레벨업에 필요한 점수
    public float spawnObjectInterval = 4f;              // 오브젝트 소환 간격
    public float spawnFishInterval = 5f;                // 물고기 소환 간격
    public float spawnGravity = 0.1f;                   // 오브젝트의 중력

    private int level;                                  // 게임의 전체 레벨
    public int Level
    {
        get { return level; }
        set
        {
            // 레벨업 할 때 마다 작동
            level = value;
            spawnObjectInterval = Mathf.Max(0.1f, spawnObjectInterval - level * 0.3f);
            spawnFishInterval = Mathf.Max(0.1f, spawnFishInterval - level * 0.2f);
            spawnGravity += level * 0.1f;

            if(level > 1 && !spawner.isFishing)
            {
                spawner.StartFishing();
            }

            levelText.text = "Level " + level;
        }
    }

    private int score;                                  // 점수 프로퍼티
    public int Score
    {
        get { return score; }
        set
        {
            // 값이 변하면 UI도 변경
            score = value;
            if(score >= levelupPoint)
            {
                Level++;
                levelupPoint += score - (score - levelupPoint);
            }
            scoreText.text = score.ToString();
        }
    }

    private int highScore;                              // 최고 점수 프로퍼티
    public int HighScore
    {
        get { return PlayerPrefs.GetInt("AmateurHighScore"); }
        set
        {
            highScore = value;
            resultHighScoreText.text = highScore.ToString();
            // 값이 변하면 UI도 변경하고 값을 저장
            PlayerPrefs.SetInt("AmateurHighScore", highScore);
        }
    }

    private void Awake()
    {
        // 싱글톤
        if(instance == null)
        {
            instance = this;
        }
        Level = 0;
        Score = 0;
        // 최고 점수가 저장된것이 없다면 최고점수를 0 으로 저장
        if(!PlayerPrefs.HasKey("AmateurHighScore"))
        {
            // 프로퍼티에서 최고 점수의 값이 입력되면 저장된다.
            HighScore = 0;
        }

        spawnObjectInterval = 4f;
        spawnFishInterval = 5f;
        spawnGravity = 0.1f;

        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        // 1 초마다 3, 2, 1, GO 순으로 오브젝트를 ON, OFF 한다.
        three.SetActive(true);
        AudioManager.instance.PlaySoundEffect(AudioManager.SFX.CountDown);
        yield return new WaitForSeconds(1);
        three.SetActive(false);

        two.SetActive(true);
        AudioManager.instance.PlaySoundEffect(AudioManager.SFX.CountDown);
        yield return new WaitForSeconds(1);
        two.SetActive(false);

        one.SetActive(true);
        AudioManager.instance.PlaySoundEffect(AudioManager.SFX.CountDown);
        yield return new WaitForSeconds(1);
        one.SetActive(false);

        go.SetActive(true);
        AudioManager.instance.PlaySoundEffect(AudioManager.SFX.CountDown);
        go.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
        yield return new WaitForSeconds(2);
        go.SetActive(false);

        // 플레이를 활성화, 움직일 수 있다.
        isPlay = true;
        // 점수 증가 코루틴 실행
        StartCoroutine(AddScore());
        // 소환기 작동 시작
        spawner.gameObject.SetActive(true);
        // 코루틴을 종료한다.
        yield break;
    }

    private IEnumerator AddScore()
    {
        while (isPlay)
        {
            // 점수 증가 간격 마다 1점 증가
            Score++;

            yield return new WaitForSeconds(scoreTime);
        }
    }

    // 오부에서 실행되는 점수 증가, 코인을 먹을때 작동한다.
    public void AddScore(int score)
    {
        Score += score;
    }

    // 게임 오버
    public void GameOver()
    {
        isLive = false;
        isPlay = false;
        
        gameOverPanel.SetActive(true);
        resultScoreText.text = Score.ToString();

        if(Score > HighScore) HighScore = Score;
        
        resultHighScoreText.text = HighScore.ToString();
    }
}
