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


    [Header("��")]
    public int birdCount = 10;

    [Header("�Ѿ�")]
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

        // ���콺 Ŀ���� �Ⱥ��̰� �ϱ�
        Cursor.visible = false;
        // ���콺 Ŀ���� ȭ�� ������ ������ �ʰ� �ϱ�
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        Init();
    }


    // �ʱ�ȭ �Ѵ�.
    private void Init()
    {
        // �Ѿ� �׸��� ���� �°� �Ѿ��� �����.
        bulletCount = bulletImages.Length;

        for(int i = 0; i < bulletCount; i++)
        {
            // �ʱ�ȭ �ϸ� �Ѿ��� �����Ǿ� �ִ� ����
            // �ƴϸ� ������ �޼��带 ����� �͵�..?
            bulletImages[i].sprite = load;
        }

        // ���� �ʱ�ȭ
        Score = 0;
    }

    // �Ѿ��� �޴������� �����ؼ� ���⿡ �������.
    // ���� ���� ���� �־�� �Ѵ�.
    public void Shooting(bool isIn, Vector2 pos, GameObject target)
    {
        // ���� �ȿ� �ְ� ���� �Ѿ��� ���� �ؾ� �Ѵ�.
        BulletDown();

        if (isIn)
        {
            // ���� ���
            ScoreUp(pos);

            // �� ����
            DestroyBird(target);
        }

        if(bulletCount == 0)
        {
            isReloading = true;
            // ������
            // x�� ���� x�� ���� �ϴ°� ������ �Ѵ�. 
            StartCoroutine(Reload());

        }
    }

    // ���� ����
    // ���߿� ���� ���� �ٸ� ����??
    public void ScoreUp(Vector2 pos)
    {
        // ���� ���̿� ���� �ٸ� ������ �ַ���?
        // 0 ~ 5

        int getScore = 0;

        if(pos.y <= 0)
        {
            // 20 ��
            getScore = 20;
        }
        else if(pos.y > 0 && pos.y < 5)
        {
            // ���
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
        // ���� ����
        GameObject flot = Instantiate(flotinPrefab, flotingParent);

        // ���� ��ġ�� ���콺 Ŭ�� �� ��ġ��
        flot.transform.position = pos;

        // ������ ���ڿ� ������ ����
        flot.GetComponent<TextMesh>().text = sco.ToString();

        // ������ ���� ����
        float x = Random.Range(-1, 1f);
        Vector2 explosionPos = new Vector2(x, 1).normalized;

        // ������ �������� ���ڸ� ����
        flot.GetComponent<Rigidbody2D>().AddForce(explosionPos * explosionPower, ForceMode2D.Impulse);
        Destroy(flot, 1f);

        // ����Ʈ ���
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

    private void DestroyBird(GameObject target)
    {
        target.gameObject.SetActive(false);
    }

    private IEnumerator Reload()
    {
        // �������� �ѹ��� �Ǵ� ���� �ƴ� ���� �ð� ���� �����Ѵ�.
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
        // �׸��� �����ؾ� �Ѵ�.
        // 3,2,1, for?? 2,1,0
        bulletImages[bulletCount].sprite = empty;
    }

    private void BulletUp()
    {
        bulletImages[bulletCount].sprite = load;
        // �׸��� �����ؾ� �Ѵ�.
        // 3,2,1, for?? 2,1,0
        bulletCount++;
    }
}
