using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playButton, scoreText, highScoreText, player, bullet, obstacle;

    private int score, highScore;

    private bool canShoot, canPlay;

    private struct SpawnData { public Vector3 start, direction; }

    readonly List<SpawnData> startPos = new List<SpawnData>()
    {
        new SpawnData()
        {
            start = new Vector3(9, 0.75f, 0), direction = new Vector3(-1,0,0)
        },
        new SpawnData()
        {
            start = new Vector3(-9, 0.75f, 0), direction = new Vector3(1,0,0)
        },
        new SpawnData()
        {
            start = new Vector3(0, 0.75f, 9), direction = new Vector3(0,0,-1)
        },
        new SpawnData()
        {
            start = new Vector3(0, 0.75f, -9), direction = new Vector3(0,0,1)
        }
    };


    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        score = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        highScoreText.GetComponent<Text>().text = "HighScore : " + highScore;
        highScoreText.SetActive(true);
        scoreText.SetActive(false);
        playButton.SetActive(true);
        canPlay = false;
        canShoot = true;
    }

    private void Update()
    {
        if (!canPlay) return;
        if(Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            GameObject temp = Instantiate(bullet);
            temp.transform.position = player.GetComponent<Player>().spwnPoint.position;
            temp.GetComponent<Bullet>().direction = player.GetComponent<Player>().spwnPoint.position - player.transform.position;
            Vector3 tempRot = temp.transform.rotation.eulerAngles;
            float z = player.transform.rotation.eulerAngles.y;
            temp.transform.eulerAngles = new Vector3(tempRot.x, tempRot.y, -z);
            StartCoroutine(ShootTimer());
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        canPlay = false;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreText.GetComponent<Text>().text = "HighScore : " + highScore;
        highScoreText.SetActive(true);

    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.GetComponent<Text>().text = "Score : " + score;
    }

    public void GamePlay()
    {
        highScoreText.SetActive(false);
        playButton.SetActive(false);
        scoreText.SetActive(true);
        scoreText.GetComponent<Text>().text = "Score : " + score;
        canPlay = true;
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while(canPlay)
        {
            GameObject temp = Instantiate(obstacle);
            SpawnData currentPos = startPos[Random.Range(0, startPos.Count)];
            temp.transform.position = currentPos.start;
            temp.GetComponent<Cube>().direction = currentPos.direction;
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
