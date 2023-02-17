using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;        // 파이어베이스 사용
using System;
using Firebase;

// 싱글톤으로 만들었다.
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
    
    private FirebaseAuth auth;      // 로그인 회원 가입 등에 사용
    private FirebaseUser user;      // 인증이 완료된 유저 정보

    public string UserId => user.UserId;        // 외부에서 유저 id를 사용하기 위해서 만든 프로퍼티
    

    public Action<bool> LoginState;

    public void Init()
    {
        // auth를 기본 값으로 설정한다.
        auth = FirebaseAuth.DefaultInstance;

        // 임시, 기존에 로그인되어 있는 것이 있을 경우 로그아웃 하는 것
        if(auth.CurrentUser != null)
        {
            LogOut();
        }

        // auth의 상태가 변하면 OnChange를 실행한다.
        auth.StateChanged += OnChange;
    }

    // 구조가 이렇게 생겼다.
    private void OnChange(object sender, EventArgs e)
    {
        if(auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if(!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState.Invoke(false);
            }

            user = auth.CurrentUser;
            if(signed)
            {
                Debug.Log("로그인");
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
                Debug.Log("회원가입 취소");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("회원가입 실패");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("회원가입 완료");
        });
    }

    public void LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("로그인 실패");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("로그인 완료");
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }
}
