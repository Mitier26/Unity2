using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class BeginGameManager : MonoBehaviour
{
    public static BeginGameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    public bool isPlay = false;


    [Header("새")]
    public int birdCount = 10;

    [Header("총알")]
    private int bulletCount;
    [SerializeField] private float reloadTime;
    public Image[] bulletImages;
    public Sprite empty, load;
    public bool isReloading = false;


    [Header("UI")]
    public GameObject flotinPrefab;
    public Transform flotingParent;
    public float explosionPower;

    [SerializeField] private GameObject explosionPrefab;

    private int score;

    public int Score
    {
        get { return score; }
        set 
        { 
            score = value; 
            scoreText.text = score.ToString();
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // 마우스 커서를 안보이게 하기
        Cursor.visible = false;
        // 마우스 커서가 화면 밖으로 나가지 않게 하기
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        Init();
    }


    // 초기화 한다.
    private void Init()
    {
        // 총알 그림의 수에 맞게 총알을 만든다.
        bulletCount = bulletImages.Length;

        for(int i = 0; i < bulletCount; i++)
        {
            // 초기화 하면 총알은 장전되어 있는 상태
            // 아니며 재장전 메서드를 만드는 것도..?
            bulletImages[i].sprite = load;
        }

        // 점수 초기화
        Score = 0;
    }

    // 총알을 메니져에서 관리해서 여기에 만들었다.
    // 새를 끄는 것을 넣어야 한다.
    public void Shooting(bool isIn, Vector2 pos, GameObject target)
    {
        // 새가 안에 있건 없건 총알을 감소 해야 한다.
        BulletDown();

        if (isIn)
        {
            // 글자 출력
            ScoreUp(pos);

            // 새 삭제
            DestroyBird(target);
        }

        if(bulletCount == 0)
        {
            isReloading = true;
            // 재장전
            // x초 동안 x개 장전 하는것 만들어야 한다. 
            StartCoroutine(Reload());

        }
    }

    // 점수 증가
    // 나중에 새에 따라 다른 점수??
    public void ScoreUp(Vector2 pos)
    {
        // 새의 높이에 따라 다른 점수를 주려면?
        // 0 ~ 5

        int getScore = 0;

        if(pos.y <= 0)
        {
            // 20 점
            getScore = 20;
        }
        else if(pos.y > 0 && pos.y < 5)
        {
            // 계산
            float cal = (float)pos.y / (float)5f;
            float result = Mathf.Lerp(19, 2, cal);
            getScore = (int)result;
        }
        else
        {
            getScore = 1;
        }

        Score += getScore;

        SetFlotingText(pos, getScore);
    }

    private void SetFlotingText(Vector2 pos, int sco)
    {
        // 글자 생성
        GameObject flot = Instantiate(flotinPrefab, flotingParent);

        // 글자 위치를 마우스 클릭 한 위치로
        flot.transform.position = pos;

        // 생성한 글자에 점수를 대입
        flot.GetComponent<TextMesh>().text = sco.ToString();

        // 랜덤한 수를 생성
        float x = Random.Range(-1, 1f);
        Vector2 explosionPos = new Vector2(x, 1).normalized;

        // 랜덤한 방향으로 글자를 날림
        flot.GetComponent<Rigidbody2D>().AddForce(explosionPos * explosionPower, ForceMode2D.Impulse);
        Destroy(flot, 1f);

        // 이펙트 출력
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

    private void DestroyBird(GameObject target)
    {
        target.gameObject.SetActive(false);
    }

    private IEnumerator Reload()
    {
        // 재장전을 한번에 되는 것이 아닌 일정 시간 동안 장전한다.
        while (bulletCount < 3)
        {
            yield return new WaitForSeconds(reloadTime);
            BulletUp();
        }

        isReloading = false;
        yield break;
    }

    private void BulletDown()
    {
        bulletCount--;
        // 그림도 변경해야 한다.
        // 3,2,1, for?? 2,1,0
        bulletImages[bulletCount].sprite = empty;
    }

    private void BulletUp()
    {
        bulletImages[bulletCount].sprite = load;
        // 그림도 변경해야 한다.
        // 3,2,1, for?? 2,1,0
        bulletCount++;
    }
}
