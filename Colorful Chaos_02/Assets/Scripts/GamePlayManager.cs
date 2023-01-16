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
    // 초기화 부분
    // 필요한 변수생성
    public static GamePlayManager Instance;
    private bool hasFinished;
    public List<Color> colors;  // 각 색을 가지고 있을 것 6개 id와 연관

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            hasFinished = false;

            // 게임 씬에 들어 오면 실행 되는 것으로 게임메니저에 게임이 실행되었다고 전한다.
            GameManager.Instance.IsInitialzed = true;

            // 화면에 출력하는 text
            score = 0;
            scoreText.text = score.ToString();

            StartCoroutine(SpawnScore());
        }
    }

    #endregion

    #region LOGIC

    // 유저의 입력을 받으면 실행 되는 영역
    [SerializeField] private ScoreEffect scoreEffect;
    private void Update()
    {
        // 입력
        if(Input.GetMouseButtonDown(0) && !hasFinished)
        {
            // 다른 곳을 클릭하면 게임 오버로 만들었다.
            // 이렇게 만들지 않아도 상관은 없다.
            if(currentScore == null)
            {
                GameEnded();
                return;
            }

            // 아래 있는 버튼 같아 보이는 것은 버튼이 아니다.
            // 해당 블럭을 클릭하는 판정을 해야 한다.

            // 마우스 위치를 알아온다.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 마우스 위치를 2D 로 변경한다.
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // 레이케스트로 물체를 판정한다.
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // 마우스를 클릭했는데 아무 것도 없는 곳에 클릭하거나.
            // 클릭한 곳에 있는 것이 블럭 이면 게임오버
            if(!hit.collider || !hit.collider.gameObject.CompareTag("Block"))
            {
                GameEnded();
                return;
            }


            // ID 비교
            int currentScoreID = currentScore.colorID;
            int clickScoreID = hit.collider.GetComponent<Player>().colorID;

            // 두개의 ID 가 같지 않다면 게임오버
            if(currentScoreID != clickScoreID)
            {
                GameEnded();
                return;
            }

            // 이후에는 클릭한 것의 ID 가 같다는 것이다.
            // 같은 것을 선택하면 에니메이션을 출력하고 사라지고
            // 점수를 증가한다.

            // 에니메이션 생성
            var t = Instantiate(scoreEffect, currentScore.gameObject.transform.position, Quaternion.identity);

            // 에니메니션 초기화
            t.Init(colors[currentScoreID]);

            // 파괴
            var tempScore = currentScore;
            if(currentScore.nextScore != null)
            {
                // 뒤에 있는 것이 있다면 뒤에 것을 지금 것으로 변경해야 한다. 중요
                currentScore = currentScore.nextScore;
            }
            Destroy(tempScore);

            // 점수 증가
            UpdateScore();
        }
    }

    #endregion

    #region SCORE
    // 위에서 내려오는 블록 생성하는 것
    // 생성된 블록은 블록마다 있는 이동명령에 따라 이동한다. 여기서는 생성만 관리
    // 생성 위치, 생성 간격, 생성할 것,
    // 랜덤으로 id 를 부여하고 id에 맞는 색으로 변경
    // 색을 변경하는 것은 블록에 만들어도 될것 같다.

    // 중요!! 메니저에서 색, 위치, id 를 지정할 것 같았지만.
    // 블럭이 생성될때 작동되게 만들었다.
    // 메니저는 생성만 한다.

    // 또 SCORE 부분에서 점수관리도 한다.
    // 화면에 표시되는 점수 내려오는 블록을 파괴했을 나오는 사운드

    private int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioClip pointSfx;
    
    // 점수 획득 사운드 출력 기능
    private void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        SoundManager.Instance.PlaySound(pointSfx);
    }

    // 소환 기능
    // 게임이 실행되면 소환은 계속 반복된다.
    // 당현히 반복되기 위해서는 실행해 주어야 한다. Awake

    [SerializeField] private float spawnTime;
    [SerializeField] private Score scorePrefab;
    private Score currentScore;
    private IEnumerator SpawnScore()
    {
        // 생성을 만들기 전에 중요한 것이 있다.
        // 게임의 판정
        // 처음 나오는 블럭과 나중에 나오는 블럭을 어떻게 구별할 것인가?
        // 리스트, 큐 등을 활용할 수 있겠지만 
        // 여기에서는 클래스의 특징을 이용하여 만든었다.

        // 생성해야 하는 것은 Score
        // Score 내부에 다음 블럭을 저장하는 것이 있다면
        // currentScore 에 처음 만든것을 넣고.
        // 처음 만든 것이 사라지면 nextScore를 이용 하여 currentScore 를 nextScore로 변경하는 것으로
        // currentScore 를 유지한다.

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
    // 게임이 끝났을 때
    // 게임이 종료되었다고 알린다.
    // 게임메니저에 획득한 점수를 보낸다.
    // 씬을 이동한다.
    // 소리를 출력한다.

    [SerializeField] private AudioClip loseSfx;
    // 유니티 이벤트
    // Score 에 있는 것을 실행한다.
    public UnityAction GameEnd;
    private void GameEnded()
    {
        hasFinished = true;
        GameManager.Instance.CurrentScore = score;
        SoundManager.Instance.PlaySound(loseSfx);
        GameEnd?.Invoke();
        Invoke("ChangeScene", 2f);
    }

    // 씬 이동은 2초후에 한다.
    // 코루틴? 인보크?
    private void ChangeScene()
    {
        GameManager.Instance.GoToMainMenu();
    }

    #endregion
}
