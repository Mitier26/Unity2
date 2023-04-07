using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive {  UnlockPotato, UnlockBean }
    Achive[] achives;

    WaitForSecondsRealtime wait;

    private void Awake()
    {
        achives =(Achive[])Enum.GetValues(typeof(Achive));

        wait = new WaitForSecondsRealtime(5f);

        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }

    }

    private void Start()
    {
        UnlockCharater();
    }

    private void UnlockCharater()
    {
        for (int i = 0; i < lockCharacter.Length;  i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    private void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch(achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.Instance.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int i = 0; i < uiNotice.transform.childCount; i++)
            {
                bool isActive = i == (int)achive;
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    private IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
