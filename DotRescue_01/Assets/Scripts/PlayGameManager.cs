using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayGameManager : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    public float speed = 0.3f;
    public float rotateTime;
    public float delay = 3;

    public TMP_Text scoreText;

    private void Update()
    {
        if (!GameManager.Instance._isPlay) return;

        rotateTime += Time.deltaTime;

        if(rotateTime > delay)
        {
            rotateTime = 0;
            delay = Random.Range(2f, 5f);
            speed *= -1;
        }
        obstacle.transform.Rotate(Vector3.forward * speed);

        scoreText.text = GameManager.Instance._score.ToString("F0");
    }

}
