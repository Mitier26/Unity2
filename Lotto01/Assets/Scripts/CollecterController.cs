using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { NONE, START,COLLECTER_ON, BALL_MOVE, CIRCLE_MOVE, END }

public class CollecterController : MonoBehaviour
{
    public State state = State.NONE;
    public Collecter collecterPrefab;
    public float collecterRadius;
    public Collecter[] collecters;
    private int collecterCount = 6;
    private int collecterNumber = 0;

    public CircleRotator rotator;
    public Lotto3UIManager uiManager;

    private void Start()
    {
        collecters = new Collecter[collecterCount];
        SetCollecter();
        Invoke(nameof(SetStart), Random.Range(5f, 8f));
    }
    private void SetCollecter()
    {
        for (int i = 0; i < collecterCount; i++)
        {
            float angle = Mathf.PI * 0.5f - i * (Mathf.PI * 2) / collecterCount;
            Collecter collecter = Instantiate(collecterPrefab, this.transform);
            collecter.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * collecterRadius;
            collecter.transform.Rotate(new Vector3(0, 0, i * -360/collecterCount), Space.Self);
            collecter.GetComponent<BoxCollider2D>().enabled = false;
            collecters[i] = collecter;
        }
    }

    private void SetStart()
    {
        SetSwitch(State.COLLECTER_ON);
    }

    public void SetSwitch(State state)
    {
        switch (state)
        {
            case State.NONE:
                Debug.Log("asdfafd");
                break;
            case State.START:
                // 대기 
                break;
            case State.COLLECTER_ON:
                collecters[collecterNumber].GetComponent<BoxCollider2D>().enabled = true;
                // 콜렉터 작동
                SetSwitch(State.NONE);
                break;
            case State.BALL_MOVE:
                // 공이 collecter 중앙으로 이동
                SetSwitch(State.CIRCLE_MOVE);
                break;
            case State.CIRCLE_MOVE:
                // 여기에서는 circle 을 알아야한다.
                // 이것이 스파게티 코드!!!
                // 공의 이동이 완료되면 회전
                // 회전을 5 번 하면 종료

                if (collecterNumber > 4)
                {
                    SetSwitch(State.END);
                }
                else
                {
                    rotator.RotateStart();
                    collecterNumber++;
                }
                break;
            case State.END:
                Debug.Log("끝");
                uiManager.gameObject.SetActive(true);
                // UI 출력
                // 다시 시작
                // 뒤로 가기
                // 번호 출력
                break;
        }
    }

}
