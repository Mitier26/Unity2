using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmateurManager : MonoBehaviour
{
    public static AmateurManager instance;              // �̱���

    public bool isLive = true;                          // �÷��̾ ����ִ��� Ȯ��
    public bool isPlay = false;                         // ������ ���� �Ǿ����� Ȯ��

    [SerializeField]
    private GameObject one, two, three, go;             // ī��Ʈ �ٿ� ������Ʈ

    [SerializeField]
    private AmateurSpawner spawner;                         // ��ȯ�⿡ ����� �����ϱ� ���� 
    
    [SerializeField]
    private GameObject gameOverPanel;                   // ���� ���� ������Ʈ

    [SerializeField]
    private TextMeshProUGUI scoreText;                  // ���� ���� ǥ�õǴ� ����
    [SerializeField]
    private TextMeshProUGUI resultScoreText;            // ��� â�� ǥ�õǴ� ����
    [SerializeField]    
    private TextMeshProUGUI resultHighScoreText;        // ��� â�� ǥ�õǴ� �ְ� ����
    [SerializeField]
    private TextMeshProUGUI levelText;                  // ���� ǥ��
   

    [SerializeField]
    private float scoreTime = 1f;                       // 1���� �ö󰡴� �ð�

    private int levelupPoint = 21;                      // �������� �ʿ��� ����
    public float spawnObjectInterval = 4f;              // ������Ʈ ��ȯ ����
    public float spawnFishInterval = 5f;                // ����� ��ȯ ����
    public float spawnGravity = 0.1f;                   // ������Ʈ�� �߷�

    private int level;                                  // ������ ��ü ����
    public int Level
    {
        get { return level; }
        set
        {
            // ������ �� �� ���� �۵�
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

    private int score;                                  // ���� ������Ƽ
    public int Score
    {
        get { return score; }
        set
        {
            // ���� ���ϸ� UI�� ����
            score = value;
            if(score >= levelupPoint)
            {
                Level++;
                levelupPoint += score - (score - levelupPoint);
            }
            scoreText.text = score.ToString();
        }
    }

    private int highScore;                              // �ְ� ���� ������Ƽ
    public int HighScore
    {
        get { return PlayerPrefs.GetInt("AmateurHighScore"); }
        set
        {
            highScore = value;
            resultHighScoreText.text = highScore.ToString();
            // ���� ���ϸ� UI�� �����ϰ� ���� ����
            PlayerPrefs.SetInt("AmateurHighScore", highScore);
        }
    }

    private void Awake()
    {
        // �̱���
        if(instance == null)
        {
            instance = this;
        }
        Level = 0;
        Score = 0;
        // �ְ� ������ ����Ȱ��� ���ٸ� �ְ������� 0 ���� ����
        if(!PlayerPrefs.HasKey("AmateurHighScore"))
        {
            // ������Ƽ���� �ְ� ������ ���� �ԷµǸ� ����ȴ�.
            HighScore = 0;
        }

        spawnObjectInterval = 4f;
        spawnFishInterval = 5f;
        spawnGravity = 0.1f;

        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        // 1 �ʸ��� 3, 2, 1, GO ������ ������Ʈ�� ON, OFF �Ѵ�.
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

        // �÷��̸� Ȱ��ȭ, ������ �� �ִ�.
        isPlay = true;
        // ���� ���� �ڷ�ƾ ����
        StartCoroutine(AddScore());
        // ��ȯ�� �۵� ����
        spawner.gameObject.SetActive(true);
        // �ڷ�ƾ�� �����Ѵ�.
        yield break;
    }

    private IEnumerator AddScore()
    {
        while (isPlay)
        {
            // ���� ���� ���� ���� 1�� ����
            Score++;

            yield return new WaitForSeconds(scoreTime);
        }
    }

    // ���ο��� ����Ǵ� ���� ����, ������ ������ �۵��Ѵ�.
    public void AddScore(int score)
    {
        Score += score;
    }

    // ���� ����
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
