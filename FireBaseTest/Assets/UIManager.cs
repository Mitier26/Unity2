using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject userDateUI;
    public GameObject scoreboardUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void ClearScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDateUI.SetActive(false);
        scoreboardUI.SetActive(false);
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() // Regester button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void UserDateScreen()
    {
        ClearScreen();
        userDateUI.SetActive(true);
    }

    public void ScoreboardScreen()
    {
        ClearScreen();
        scoreboardUI.SetActive(true);
    }
}
