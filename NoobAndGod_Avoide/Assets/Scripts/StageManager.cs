using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI noobScore;
    public TextMeshProUGUI beginnerScore;
    public TextMeshProUGUI AmateurScore;
    public TextMeshProUGUI ProfessionalScore;
    public TextMeshProUGUI godScore;

    private void Start()
    {
        noobScore.text = (PlayerPrefs.HasKey(Constants.NoobSaveString) == false ? 0 : PlayerPrefs.GetInt(Constants.NoobSaveString)).ToString();
        beginnerScore.text = (PlayerPrefs.HasKey(Constants.BginnerSaveString) == false ? 0 : PlayerPrefs.GetInt(Constants.BginnerSaveString)).ToString();
        AmateurScore.text = (PlayerPrefs.HasKey(Constants.AmateurSaveString) == false ? 0 : PlayerPrefs.GetInt(Constants.AmateurSaveString)).ToString();
        ProfessionalScore.text = (PlayerPrefs.HasKey(Constants.ProfessionalSaveString) == false ? 0 : PlayerPrefs.GetInt(Constants.ProfessionalSaveString)).ToString();
        godScore.text = (PlayerPrefs.HasKey(Constants.GodSaveString) == false ? 0 : PlayerPrefs.GetInt(Constants.GodSaveString)).ToString();

        FireBaseManager3.instance.LoadRank(Constants.NoobSaveString);
    }
}
