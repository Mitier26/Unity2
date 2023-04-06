using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class GodGameManager : MonoBehaviour
{
    public static GodGameManager Instance;

    public int characterId;         // ���õ� �ɸ����� ��ȣ

    [SerializeField]
    private GameObject player;      // �÷��̾�

    public bool isOpening;          // ������ ������
    public bool isStart;            // ������ ����


    [SerializeField]                // �÷��̾��� �׸��� �ٲپ��� Animator
    private RuntimeAnimatorController[] animationControllers;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private SpriteShapeController groundController;
    private Vector2[] groundPoints;
    private float[] groundPointYOffsets;
    

    private int score;
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

    private int highScore;
    public int HighScore
    {
        get { return highScore; }
        set {  highScore = value;}
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;


        isOpening = false;
        isStart = false;

        Score = 0;
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
        player.GetComponent<Animator>().runtimeAnimatorController = animationControllers[characterId];
        player.transform.position = pos;
        player.SetActive(true);
        scoreText.gameObject.SetActive(true);

        StartCoroutine(Scoring());
    }

    public void GameOver()
    {
        isStart = false;
        player.SetActive(false);
    }

    private IEnumerator Scoring()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.7f);
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
