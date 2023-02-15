using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogInSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public TMP_Text outputText;

    private void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangeedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangeedState(bool sign)
    {
        outputText.text = sign ? "로그인" : "로그아웃";
        outputText.text += FirebaseAuthManager.Instance.UserId;
    }

    public void Create()
    {
        string e = email.text;
        string p = password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }

    public void LogIn()
    {
        FirebaseAuthManager.Instance.LogIn(email.text, password.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
