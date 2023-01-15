using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Vector3> spawnPos;

    [HideInInspector] public int colorId;
    [HideInInspector] public Score NextScore;

    private bool hasGameFinished;
    private void Awake()
    {
        hasGameFinished = false;
        transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
        int colorCount = GamePlayManager.Instance.colors.Count;
        colorId = Random.Range(0, colorCount);
        GetComponent<SpriteRenderer>().color = GamePlayManager.Instance.colors[colorId];
    }

    private void FixedUpdate()
    {
        if (hasGameFinished) return;
        transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.down);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            GamePlayManager.Instance.GameEnded();
        }
    }

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
