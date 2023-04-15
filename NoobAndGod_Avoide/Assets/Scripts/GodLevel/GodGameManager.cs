using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class GodGameManager : MonoBehaviour
{
    public static GodGameManager Instance;              // 싱글톤
    [SerializeField]
    private GodCameraController cam;                    // 카메라

    public int characterId;                             // 선택된 케릭터의 번호

    [SerializeField]
    private GameObject player;                          // 플레이어
    public GameObject jumpButton;
    public GameObject joystick;

    public bool isOpening;                              // 게임의 오프닝
    public bool isStart;                                // 게임의 시작


    [SerializeField]                                    // 플레이어의 그림을 바꾸어줄 Animator
    private RuntimeAnimatorController[] animationControllers;

    [SerializeField]
    private TextMeshProUGUI scoreText;                  // 점수 표시 텍스트
    [SerializeField]
    private TextMeshProUGUI highScoreText;              // 결과 창의 최고 점수
    [SerializeField]
    private TextMeshProUGUI resultScoreText;            // 결과 창의 현재 점수
    [SerializeField]
    private GameObject gameoverPanel;                     // 결과 창

    [SerializeField]
    private SpriteShapeController groundController;     // 바닥
    private Vector2[] groundPoints;                     // 바닥의 포인트
    private float[] groundPointYOffsets;                // 바닥의 이동 간격

    private int score;                                  // 점수
    public int Score
    {
        get { return score; }
        set 
        {
            score = value; 
            scoreText.text = score.ToString();

            if (score % 13 == 0 && score != 0) StartCoroutine(ChangeGround());
        }
    }

    private int highScore;                              // 최고 점수
    public int HighScore
    {
        get { return highScore; }
        set 
        { 
            highScore = value;
            FireBaseManager3.instance.SaveData(Constants.GodSaveString, highScore);
        }
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;


        isOpening = false;
        isStart = false;

        Score = 0;
        Time.timeScale = 1f;

        if (!PlayerPrefs.HasKey(Constants.GodSaveString)) PlayerPrefs.SetInt(Constants.GodSaveString, 0);
    }

    private void Start()
    {
        groundPoints = new Vector2[groundController.spline.GetPointCount() -2];
        groundPointYOffsets = new float[groundPoints.Length];

        for(int i = 0; i < groundPoints.Length; i++)
        {
            groundPoints[i] = groundController.spline.GetPosition(i);
        }

    }

    public void GameStart(int id, Vector2 pos)
    {
        characterId = id;
        isStart = true;
        player.GetComponent<GodPlayer2>().animator.runtimeAnimatorController = animationControllers[characterId];
        player.transform.position = pos;
        cam.SetOffset();
        player.SetActive(true);
        joystick.SetActive(true);
        jumpButton.SetActive(true);
        scoreText.gameObject.SetActive(true);

        StartCoroutine(Scoring());
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        
        if(Score > highScore)
        {
            HighScore = Score;
        }

        joystick.SetActive(false); 
        jumpButton.SetActive(false);

        gameoverPanel.SetActive(true);
        highScoreText.text = HighScore.ToString();
        resultScoreText.text = Score.ToString();

        isStart = false;
        player.SetActive(false);
    }

    private IEnumerator Scoring()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.4f);
            Score++;
        }
    }

    private IEnumerator ChangeGround()
    {
        float groundChangeTime = 1f;
        float elapsed = 0;

        for(int i = 0; i < groundPoints.Length; i++)
        {
            groundPointYOffsets[i] = Random.Range(-1.5f, 1.5f);
            groundPoints[i] = groundController.spline.GetPosition(i);
        }


        while( elapsed < groundChangeTime)
        {
            float percent = elapsed / groundChangeTime;

            for(int i = 0; i < groundPoints.Length; i++)
            {
                Vector2 point = groundPoints[i];
                point.y = Mathf.Lerp(point.y, point.y + groundPointYOffsets[i], percent);
                point.y = Mathf.Clamp(point.y, -1.5f, 1.5f);
                groundController.spline.SetPosition(i, point);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
