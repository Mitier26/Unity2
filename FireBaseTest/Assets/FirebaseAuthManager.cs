using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;        // ���̾�̽� ���
using System;
using Firebase;

// �̱������� �������.
public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            } 

            return instance;
        }
    }
    
    private FirebaseAuth auth;      // �α��� ȸ�� ���� � ���
    private FirebaseUser user;      // ������ �Ϸ�� ���� ����

    public string UserId => user.UserId;        // �ܺο��� ���� id�� ����ϱ� ���ؼ� ���� ������Ƽ
    

    public Action<bool> LoginState;

    public void Init()
    {
        // auth�� �⺻ ������ �����Ѵ�.
        auth = FirebaseAuth.DefaultInstance;

        // �ӽ�, ������ �α��εǾ� �ִ� ���� ���� ��� �α׾ƿ� �ϴ� ��
        if(auth.CurrentUser != null)
        {
            LogOut();
        }

        // auth�� ���°� ���ϸ� OnChange�� �����Ѵ�.
        auth.StateChanged += OnChange;
    }

    // ������ �̷��� �����.
    private void OnChange(object sender, EventArgs e)
    {
        if(auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if(!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState.Invoke(false);
            }

            user = auth.CurrentUser;
            if(signed)
            {
                Debug.Log("�α���");
                LoginState.Invoke(true);
            }
        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => 
        { 
            if(task.IsCanceled)
            {
                Debug.Log("ȸ������ ���");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("ȸ������ ����");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("ȸ������ �Ϸ�");
        });
    }

    public void LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("�α��� ����");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("�α��� �Ϸ�");
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
