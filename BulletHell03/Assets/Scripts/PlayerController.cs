using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData;                        // 스테이지 데이터를 가지고 오기 위한것
    private Movement2D movement2D;                      // 이동하기

    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;      // 공격키 설정
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;
    private Weapon weapon;
   

    private bool isDie = false;
    private Animator animator;

    private int score;
    public int Score
    {
        // 점수를 저장할 때 점수가 음수가 되지 않도록 설정한다.
        // 프로퍼티로 만들면 if 문을 사용하지 않고도 만들 수 있다.
        set => score = Mathf.Max(0, value);
        get => score;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDie) return;

        // 이동하려고 하는 방향을 지정해준다.
        // GetAxisRaw를 이용해 상하좌우를 받는다.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // movement2D에 방향값을 전해 준다.
        movement2D.MoveTo(new Vector3(x, y, 0));

        // 버튼이 눌러지면 공격
        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if(Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }

        if(Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
        // 플레이어가 화면 밖으로 나가지 못하기 위한 것
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
            Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y), 0);
    }

    public void OnDie()
    {
        movement2D.MoveTo(Vector3.zero);
        animator.SetTrigger("OnDie");
        Destroy(GetComponent<CircleCollider2D>());
        isDie = true;
    }

    public void OnDieEvent()
    {
        // 점수의 저장
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(nextSceneName);
    }
}
