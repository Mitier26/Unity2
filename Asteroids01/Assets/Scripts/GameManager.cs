using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public int lives = 3;
    public float spawnDelay = 3.0f;

    public int score = 0;

    public ParticleSystem explosion;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    
    public void PlayerDied()
    {
        lives--;
        livesText.text = "LIVES : " + lives.ToString();

        Asteroid[] asteroid = FindObjectsOfType<Asteroid>();
        
        for(int i = 0; i < asteroid.Length; i++)
        {
            Destroy(asteroid[i].gameObject);
        }

        explosion.transform.position = player.transform.position;
        explosion.Play();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), spawnDelay);
        }
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {

    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        if(asteroid.size < 0.8f)
        {
            score += 100;
        }
        else if(asteroid.size < 1.2f)
        {
            score += 60;
        }
        else
        {
            score += 15;
        }

        scoreText.text = "Score " + score.ToString();
    }
}
