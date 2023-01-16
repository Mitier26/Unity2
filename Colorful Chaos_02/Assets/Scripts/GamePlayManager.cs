using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GamePlayManager : MonoBehaviour
{
    #region START
    // �ʱ�ȭ �κ�
    // �ʿ��� ��������
    public static GamePlayManager Instance;
    private bool hasFinished;
    public List<Color> colors;  // �� ���� ������ ���� �� 6�� id�� ����

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            hasFinished = false;

            // ���� ���� ��� ���� ���� �Ǵ� ������ ���Ӹ޴����� ������ ����Ǿ��ٰ� ���Ѵ�.
            GameManager.Instance.IsInitialzed = true;

            // ȭ�鿡 ����ϴ� text
            score = 0;
            scoreText.text = score.ToString();

            StartCoroutine(SpawnScore());
        }
    }

    #endregion

    #region LOGIC

    // ������ �Է��� ������ ���� �Ǵ� ����
    [SerializeField] private ScoreEffect scoreEffect;
    private void Update()
    {
        // �Է�
        if(Input.GetMouseButtonDown(0) && !hasFinished)
        {
            // �ٸ� ���� Ŭ���ϸ� ���� ������ �������.
            // �̷��� ������ �ʾƵ� ����� ����.
            if(currentScore == null)
            {
                GameEnded();
                return;
            }

            // �Ʒ� �ִ� ��ư ���� ���̴� ���� ��ư�� �ƴϴ�.
            // �ش� ���� Ŭ���ϴ� ������ �ؾ� �Ѵ�.

            // ���콺 ��ġ�� �˾ƿ´�.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ���콺 ��ġ�� 2D �� �����Ѵ�.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // �����ɽ�Ʈ�� ��ü�� �����Ѵ�.
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // ���콺�� Ŭ���ߴµ� �ƹ� �͵� ���� ���� Ŭ���ϰų�.
            // Ŭ���� ���� �ִ� ���� �� �̸� ���ӿ���
            if(!hit.collider || !hit.collider.gameObject.CompareTag("Block"))
            {
                GameEnded();
                return;
            }


            // ID ��
            int currentScoreID = currentScore.colorID;
            int clickScoreID = hit.collider.GetComponent<Player>().colorID;

            // �ΰ��� ID �� ���� �ʴٸ� ���ӿ���
            if(currentScoreID != clickScoreID)
            {
                GameEnded();
                return;
            }

            // ���Ŀ��� Ŭ���� ���� ID �� ���ٴ� ���̴�.
            // ���� ���� �����ϸ� ���ϸ��̼��� ����ϰ� �������
            // ������ �����Ѵ�.

            // ���ϸ��̼� ����
            var t = Instantiate(scoreEffect, currentScore.gameObject.transform.position, Quaternion.identity);

            // ���ϸ޴ϼ� �ʱ�ȭ
            t.Init(colors[currentScoreID]);

            // �ı�
            var tempScore = currentScore;
            if(currentScore.nextScore != null)
            {
                // �ڿ� �ִ� ���� �ִٸ� �ڿ� ���� ���� ������ �����ؾ� �Ѵ�. �߿�
                currentScore = currentScore.nextScore;
            }
            Destroy(tempScore);

            // ���� ����
            UpdateScore();
        }
    }

    #endregion

    #region SCORE
    // ������ �������� ��� �����ϴ� ��
    // ������ ����� ��ϸ��� �ִ� �̵���ɿ� ���� �̵��Ѵ�. ���⼭�� ������ ����
    // ���� ��ġ, ���� ����, ������ ��,
    // �������� id �� �ο��ϰ� id�� �´� ������ ����
    // ���� �����ϴ� ���� ��Ͽ� ���� �ɰ� ����.

    // �߿�!! �޴������� ��, ��ġ, id �� ������ �� ��������.
    // ���� �����ɶ� �۵��ǰ� �������.
    // �޴����� ������ �Ѵ�.

    // �� SCORE �κп��� ���������� �Ѵ�.
    // ȭ�鿡 ǥ�õǴ� ���� �������� ����� �ı����� ������ ����

    private int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip pointSfx;
    
    // ���� ȹ�� ���� ��� ���
    private void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        SoundManager.Instance.PlaySound(pointSfx);
    }

    // ��ȯ ���
    // ������ ����Ǹ� ��ȯ�� ��� �ݺ��ȴ�.
    // ������ �ݺ��Ǳ� ���ؼ��� ������ �־�� �Ѵ�. Awake

    [SerializeField] private float spawnTime;
    [SerializeField] private Score scorePrefab;
    private Score currentScore;
    private IEnumerator SpawnScore()
    {
        // ������ ����� ���� �߿��� ���� �ִ�.
        // ������ ����
        // ó�� ������ ���� ���߿� ������ ���� ��� ������ ���ΰ�?
        // ����Ʈ, ť ���� Ȱ���� �� �ְ����� 
        // ���⿡���� Ŭ������ Ư¡�� �̿��Ͽ� �������.

        // �����ؾ� �ϴ� ���� Score
        // Score ���ο� ���� ���� �����ϴ� ���� �ִٸ�
        // currentScore �� ó�� ������� �ְ�.
        // ó�� ���� ���� ������� nextScore�� �̿� �Ͽ� currentScore �� nextScore�� �����ϴ� ������
        // currentScore �� �����Ѵ�.

        Score prevScore = null;

        while (!hasFinished)
        {
            var tempScore = Instantiate(scorePrefab);

            if(prevScore == null)
            {
                prevScore = tempScore;
                currentScore = prevScore;
            }
            else
            {
                prevScore.nextScore = tempScore;
                prevScore = tempScore;
            }

            yield return new WaitForSeconds(spawnTime);
        }

    }

    #endregion


    #region GAEMOVER
    // ������ ������ ��
    // ������ ����Ǿ��ٰ� �˸���.
    // ���Ӹ޴����� ȹ���� ������ ������.
    // ���� �̵��Ѵ�.
    // �Ҹ��� ����Ѵ�.

    [SerializeField] private AudioClip loseSfx;
    // ����Ƽ �̺�Ʈ
    // Score �� �ִ� ���� �����Ѵ�.
    public UnityAction GameEnd;
    private void GameEnded()
    {
        hasFinished = true;
        GameManager.Instance.CurrentScore = score;
        SoundManager.Instance.PlaySound(loseSfx);
        GameEnd?.Invoke();
        Invoke("ChangeScene", 2f);
    }

    // �� �̵��� 2���Ŀ� �Ѵ�.
    // �ڷ�ƾ? �κ�ũ?
    private void ChangeScene()
    {
        GameManager.Instance.GoToMainMenu();
    }

    #endregion
}
