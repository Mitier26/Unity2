using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float _score;

    public bool _isPlay = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }

        if(PlayerPrefs.HasKey(_bestScoreKey) == false)
        {
            SaveBest(0);
        }
    }

    private readonly string _bestScoreKey = "BestScore";

    public void SaveBest(int best)
    {
        PlayerPrefs.SetInt(_bestScoreKey, best);
    }

    public int LoadBest()
    {
        return PlayerPrefs.GetInt(_bestScoreKey, 0);
    }

    private void Update()
    {
        if(_isPlay)
        {
            _score += Time.deltaTime;
        }
    }

}
