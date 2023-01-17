using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] TMP_Text scoreText, endScoreText, bestScoreText;           // ȭ�鿡 �������� ����

    private int score;                                                          // ����

    [SerializeField] private Animator scoreAnimator;                            // ������ ȹ���ϸ� scoreText�� �����ϴ� Animation
    [SerializeField] private AnimationClip scoreClip;                           // � ���ϸ��̼��� ������� ���Ѵ�.
    [SerializeField] private Obstacle targetPrefab;                             // ��ȯ�� ��
    [SerializeField] private float maxSpawnOffset;                              // ��ȯ�� ���� Ÿ����ġ
    [SerializeField] private Vector3 startTargetPos;                            // Ÿ���� �ʱ� ��ġ
    [SerializeField] private Image soundImage;                                  // �׸��� ������ ��
    [SerializeField] private Sprite activeSoundSprite, inactiveSoundSprite;     // ������ �׸�
    [SerializeField] private GameObject endPanel;                               // ���� ���� �� ��µǴ� ȭ��

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
        SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        // ���� ��ȯ�Ѵ�.
        Obstacle temp = Instantiate(targetPrefab);
        // ó�� ��ġ�� 0,0,0 �̴�
        Vector3 tempPos = startTargetPos;
        // ��ȯ�� �Ǹ� ������ ��ġ�� �����Ѵ�.
        // ó�� ��ȯ�Ǵ� ���� 0,0,0 ��ġ�� ���� ���� ���ΰ� ����.
        startTargetPos.x = Random.Range(-maxSpawnOffset, maxSpawnOffset);
        // ��ȯ�Ǵ� �Ϳ� ��ǥ��ġ�� �˷��ش�.
        temp.MoveToPos(tempPos);
        // ��ǥ ��ġ�� �˷��ָ� ��ȯ �Ȱ��� �˾Ƽ� �̵��Ѵ�.
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
        // ����� �޴������� ����
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTING_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTING_SOUND) : 1) == 1;

        sound = !sound;
        soundImage.sprite = sound ? activeSoundSprite : inactiveSoundSprite;
        PlayerPrefs.SetInt(Constants.DATA.SETTING_SOUND, sound ? 1 : 0);
        AudioManager.Instance.ToggleSound();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        // ���ϸ��̼� �����ϴ� ���� ����ߴ�.
        // �۵��� ���ϸ޴ϼ��� �̸�, ���̾�, �븻�ð�??(�������ΰ� ����)
        scoreAnimator.Play(scoreClip.name, -1, 0f);
        SpawnObstacle();
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        endScoreText.text = score.ToString();

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTING_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTING_SOUND) : 1) == 1;

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
