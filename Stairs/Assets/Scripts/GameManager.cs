using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	public bool hasGameStarted;
	
	[SerializeField]
	private GameObject startButton, endPanel, blockPrefab, player;
	
	[SerializeField]
	private TMP_Text scoreText, diamondText, highScoreText, highScoreEndTest;
	
	[SerializeField]
	private Vector3 startPos, offset;
	
	private int score, diamondCount, combo, highScore;
	
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
		hasGameStarted = false;
	}
	
	private void Start()
	{
		startButton.SetActive(true);
		endPanel.SetActive(false);
		highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
		diamondCount = PlayerPrefs.HasKey("Diamonds") ? PlayerPrefs.GetInt("Diamons") : 0;
		
		scoreText.text = score.ToString();
		diamondText.text = diamondCount.ToString();
		highScoreText.text = "Best : " + highScore.ToString();
		
		for(int i = 0; i < 5; i++)
		{
			SpawnBlock();
		}
	}
	
	public void SpawnBlock()
	{
		GameObject tempBlock = Instantiate(blockPrefab);
		startPos += offset;
		tempBlock.transform.position = startPos;
	}
	
	public void UpdateDiamond()
	{
		diamondCount++;
		PlayerPrefs.SetInt("Diamond", diamondCount);
		diamondText.text = diamondCount.ToString();
	}
	
	public void UpdateScore()
	{
		combo = 1;
		score++;
		scoreText.text = score.ToString();
	}
	
	public void UpdateCombo()
	{
		combo++;
		score += combo;
		scoreText.text = score.ToString();
	}
	
	public void GameOver()
	{
		Time.timeScale = 0;
		endPanel.SetActive(true);
		if(score > highScore)
		{
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
		}
		highScoreText.text = "Best : " + highScore.ToString();
	}
	
	public void GameStart()
	{
		startButton.SetActive(false);
		player.GetComponent<Player>().hasGameStarted = true;
		hasGameStarted = true;
	}
	
	public void GameRestart()
	{
		SceneManager.LoadScene(0);
	}
	
	public void GameQuit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying	 = false;
		#endif
		Application.Quit();
	}
}
