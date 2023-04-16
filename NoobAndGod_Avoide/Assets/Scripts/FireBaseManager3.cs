using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class FireBaseManager3 : MonoBehaviour
{
    private readonly string USERS_STRING = "Users";

    public static FireBaseManager3 instance;

    // ���̾� ���̽��� ����ϱ� ���� �⺻
    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference reference;
    DependencyStatus dependencyStatus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitialzeFirebase();
                auth.StateChanged += Auth_StateChanged;
                Auth_StateChanged(this, null);
            }
        });

    }

    private void Auth_StateChanged(object sender, EventArgs e)
    {
        if (user != null)
        {
            if(user.UserId != auth.CurrentUser.UserId)
            {
                CreateAuth();
            }
        }
        else 
        {
            user = auth.CurrentUser;

            if (user == null)
            {
                CreateAuth();
            }
        }
    }

    private void InitialzeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    private void CreateAuth()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                user = task.Result;
                MakeData();
            }
            else
            {
                Debug.Log("Failed to create anonymous user: " + task.Exception);
            }
        });
    }

    public void MakeData()
    {
        reference.Child(USERS_STRING).Child(Constants.NoobSaveString).Child(user.UserId).SetValueAsync(0);
        reference.Child(USERS_STRING).Child(Constants.BginnerSaveString).Child(user.UserId).SetValueAsync(0);
        reference.Child(USERS_STRING).Child(Constants.AmateurSaveString).Child(user.UserId).SetValueAsync(0);
        reference.Child(USERS_STRING).Child(Constants.ProfessionalSaveString).Child(user.UserId).SetValueAsync(0);
        reference.Child(USERS_STRING).Child(Constants.GodSaveString).Child(user.UserId).SetValueAsync(0);
    }

    public void SaveData(string levelString, int score)
    {
        PlayerPrefs.SetInt(levelString, score);

        reference.Child(USERS_STRING).Child(levelString).Child(user.UserId).SetValueAsync(score);
    }    

    public String LoadRank(string levelString)
    {
        string rank = "";

        if(PlayerPrefs.GetInt(levelString) ==0)
        {
            rank = "----";
        }
        FirebaseDatabase database = FirebaseDatabase.DefaultInstance;
        DatabaseReference reference = database.GetReference("Users").Child(levelString);

        // ������ ���� ��������
        int myScore = PlayerPrefs.GetInt(levelString);

        // ������ ��� ����
        Query query = reference.OrderByValue().EndAt(myScore);
        query.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // ���� ó��
            }
            else if (task.IsCompleted)
            {
                // ��� ���
                // �̰��� �׳� ��ü ������ ���� ��Ÿ����.
                 rank = ((int)task.Result.ChildrenCount).ToString();
             
                Debug.Log("My rank in level " + levelString + " is " + rank);
            }
        });

        return rank;
    }
}
