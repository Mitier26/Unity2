using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoobGamaManager : MonoBehaviour
{
	NoobTarget target;
    public TextMeshProUGUI scoreText;
	public TextMeshProUGUI levelText;

	private int score;

	public int Score
	{
		get { return score; }
		set 
		{ 
			score = value; 
			scoreText.text = score.ToString();

			if(score != 0 && score % 2 == 0)
			{
				LevelUp();
			}
		}
	}

	private int highscore;

	public int HighScore
	{
		get { return highscore; }
		set 
		{ 
			highscore = value;
			PlayerPrefs.SetInt("HighScore", value);
		}
	}

	private int level = 1;

	public int Level
	{
		get { return level; }
		set
		{
			level = value;
			levelText.text = "Level : " + level;
		}
	}

    private void Start()
    {
		Score = 0;

		target = GameObject.FindGameObjectWithTag("Target").GetComponent<NoobTarget>();
    }
    private void LevelUp()
	{
		Level++;
		target.SetScale(Level);
		target.Reposition();
	}

}
