using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // 위에서 내려오는 블록
    // GamePlayManager 에서는 생성만 하고 색, 위치
    // 블럭이 생성될 때 정해준다.

    
    [SerializeField] List<Vector3> spawnPos;    // 블럭들이 생성될 위치
    [SerializeField] private float moveSpeed;   // 블럭이 아래로 이동할 속도
    [SerializeField] public int colorID;       // 블럭의 색을 정하는 ID
    public Score nextScore;                    // 게임로직을 위한 다음 블럭

    private bool hasGameFinished;

    // 블럭은 생성과 동시에 부여되는 ID 에 따라 색 이 결정된다.
    // 위치는 랜덤
    private void Awake()
    {
        transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
        // 생성되는 위치와 생성되는 색의 수가 같다는 전체에 만들었는데 잘못된것이다.
        // GamaPlayManager 에 있는  colors 를 갯수를 이용해야 완벽해진다.
        // colorID = Random.Range(0, spawnPos.Count);
        colorID = Random.Range(0, GamePlayManager.Instance.colors.Count);
        GetComponent<SpriteRenderer>().color = GamePlayManager.Instance.colors[colorID];

        hasGameFinished = false;
    }

    // 생성된 것은 아래로 이동한다.

    private void FixedUpdate()
    {
        if (hasGameFinished) return;
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    // 생성되었을 때 이벤트를 등록하고
    // 사라지면 이벤트를 삭제 한다.
    // 이벤트가 있어야한다.
    // 이벤트각 GamePlayManager에 만든다.
    private void OnEnable()
    {
        GamePlayManager.Instance.GameEnd += GameEnded;
    }

    private void OnDisable()
    {
        GamePlayManager.Instance.GameEnd -= GameEnded;
    }

    private void GameEnded()
    {
        hasGameFinished = true;
    }
}
