using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    [SerializeField]
    private float deathZone = -5.5f;
    [SerializeField]
    private float moveVelocity = 5f;

    private Rigidbody2D rb;

    private int score = 0;
    private int lives = 5;

    [SerializeField]
    private TMP_Text scoreText;

    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    private int brickCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
        rb.velocity = Vector2.down * 5f;
    }

    private void Update()
    {
        if(rb.velocity.magnitude > moveVelocity)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveVelocity);
        }

        if(transform.position.y < deathZone)
        {
            if(lives <= 0)
            {
                GameOver();
            }
            else
            {
                transform.position = Vector2.zero;
                rb.velocity = Vector2.down * 5f;
                lives--;
                livesImage[lives].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score+=10;
            scoreText.text = score.ToString("00000");
            brickCount--;

            if(brickCount <= 0)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
