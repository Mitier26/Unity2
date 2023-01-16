using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // ������ �������� ���
    // GamePlayManager ������ ������ �ϰ� ��, ��ġ
    // ���� ������ �� �����ش�.

    
    [SerializeField] List<Vector3> spawnPos;    // ������ ������ ��ġ
    [SerializeField] private float moveSpeed;   // ���� �Ʒ��� �̵��� �ӵ�
    [SerializeField] public int colorID;       // ���� ���� ���ϴ� ID
    public Score nextScore;                    // ���ӷ����� ���� ���� ��

    private bool hasGameFinished;

    // ���� ������ ���ÿ� �ο��Ǵ� ID �� ���� �� �� �����ȴ�.
    // ��ġ�� ����
    private void Awake()
    {
        transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
        // �����Ǵ� ��ġ�� �����Ǵ� ���� ���� ���ٴ� ��ü�� ������µ� �߸��Ȱ��̴�.
        // GamaPlayManager �� �ִ�  colors �� ������ �̿��ؾ� �Ϻ�������.
        // colorID = Random.Range(0, spawnPos.Count);
        colorID = Random.Range(0, GamePlayManager.Instance.colors.Count);
        GetComponent<SpriteRenderer>().color = GamePlayManager.Instance.colors[colorID];

        hasGameFinished = false;
    }

    // ������ ���� �Ʒ��� �̵��Ѵ�.

    private void FixedUpdate()
    {
        if (hasGameFinished) return;
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    // �����Ǿ��� �� �̺�Ʈ�� ����ϰ�
    // ������� �̺�Ʈ�� ���� �Ѵ�.
    // �̺�Ʈ�� �־���Ѵ�.
    // �̺�Ʈ�� GamePlayManager�� �����.
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
