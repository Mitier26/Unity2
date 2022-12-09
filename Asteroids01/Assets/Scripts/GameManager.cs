using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int lives = 3;
    public float spawnDelay = 3.0f;

    public ParticleSystem explosion;

    public void PlayerDied()
    {
        lives--;

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

    }
}
