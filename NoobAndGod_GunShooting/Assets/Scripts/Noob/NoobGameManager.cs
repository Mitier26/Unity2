using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoobGameManager : MonoBehaviour
{
	public static NoobGameManager instance;
	public NoobTarget target;

	[Header("UI")]
    public TextMeshProUGUI scoreText;
	public TextMeshProUGUI comboText;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI highScoreText;
	public TextMeshProUGUI resultScoreText;
	public GameObject menu;
	public GameObject inGame;

	[SerializeField]
	private int gameTime = 30;
	public int curTime;
	
	public int maxCombo = 30;

	public int minScore = 1;
	public int maxScore = 30;

	private int score;

	public bool isOver = true;

	public int Score
	{
		get { return score; }
		set 
		{ 
			score = value; 
			scoreText.text = score.ToString();
		}
	}

	private int highscore;

	public int HighScore
	{
		get { return PlayerPrefs.GetInt("HighScore"); }
		set 
		{ 
			PlayerPrefs.SetInt("HighScore", value);
		}
	}

	private int combo = 0;

	public int Combo
	{
		get { return combo; }
		set
		{
			combo = Mathf.Min(value, maxCombo);
			comboText.text = "Combo_" + combo;
		}
	}

    public int CurTime
    {
        get { return curTime; }
        set
        {
            curTime = value;
            timeText.text = "T : " + curTime;

			if (curTime <= 0)
			{
				GameOver();

			}
        }
    }

    private void Start()
    {
		if(instance == null)
		{
			instance = this;
		}

		target = GameObject.FindGameObjectWithTag("Target").GetComponent<NoobTarget>();

		Init();	
    }
    public void ComboUp()
	{
		Combo++;
		target.Reposition();
	}

	private IEnumerator Timer()
	{
		while(true)
		{
            yield return new WaitForSeconds(1);
            CurTime--;
        }
	}

	private void Init()
	{
		Score = 0;
		Combo = 0;
		CurTime = gameTime;
		
		target.gameObject.SetActive(false);
		target.Init();

		highScoreText.text = HighScore.ToString();

		StopAllCoroutines();
	}

	public void GameStart()
	{
		isOver = false;
		Init();
        inGame.SetActive(true);
        menu.SetActive(false);
        target.gameObject.SetActive(true);
		NoobAudioManager.instance.PlaySfx(NoobAudioManager.NOOBSFX.Reload);
        StartCoroutine(Timer());
    }

	private void GameOver()
	{
		isOver = true;
		inGame.SetActive(false);
		menu.SetActive(true);

		resultScoreText.text = Score.ToString();
		if(Score > PlayerPrefs.GetInt("HighScore"))
		{
			HighScore = Score;
		}
        highScoreText.text = HighScore.ToString();
    }
}
