using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] TMP_Text scoreText, endScoreText, bestScoreText;           // 화면에 보여지는 문자

    private int score;                                                          // 점수

    [SerializeField] private Animator scoreAnimator;                            // 점수를 획득하면 scoreText에 적용하는 Animation
    [SerializeField] private AnimationClip scoreClip;                           // 어떤 에니메이션을 출력할지 정한다.
    [SerializeField] private GameObject scorePrefab;                             // 소환될 것
    [SerializeField] private float maxSpawnOffset;                              // 소환된 것의 타켓위치
    [SerializeField] private Vector3 startTargetPos;                            // 타겟의 초기 위치
    [SerializeField] private Image soundImage;                                  // 그림을 변경할 것
    [SerializeField] private Sprite activeSoundSprite, inactiveSoundSprite;     // 변경할 그림
    [SerializeField] private GameObject endPanel;                               // 게임 종료 시 출력되는 화면

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        endPanel.SetActive(false);
        AudioManager.Instance.AddButtonSound();
        score = 0;
        scoreText.text = score.ToString();
        scoreAnimator.Play(scoreClip.name, -1, 0f);
        UpdateScorePrefab();
    }

    private void UpdateScorePrefab()
    {
        float currentRotation = scorePrefab.transform.rotation.eulerAngles.z;
        currentRotation = Mathf.Abs(currentRotation) < 0.01f ? 180 : 0f;
        Vector3 newRotation = new Vector3(0, 0, currentRotation);
        scorePrefab.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.DATA.MAIN_MENU_SCENE);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(Constants.DATA.GAMEPLAY_SCENE);
    }

    public void ToggleSound()
    {
        // 오디오 메니저에서 정리
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 1;

        sound = !sound;
        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);
        AudioManager.Instance.ToggleSound();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        // 에니메이션 블랜딩하는 것을 사용했다.
        // 작동할 에니메니션의 이름, 레이어, 노말시간??(딜레이인것 같다)
        scoreAnimator.Play(scoreClip.name, -1, 0f);
        UpdateScorePrefab();
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        endScoreText.text = score.ToString();

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 1;

        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;

        int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE) : 0;

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
            bestScoreText.text = "NEW BEST";
        }
        else
        {
            bestScoreText.text = "BEST" + highScore.ToString();
        }
    }
}
