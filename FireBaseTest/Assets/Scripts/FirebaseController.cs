using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System;
using System.Threading.Tasks;

public class FirebaseController : MonoBehaviour
{
    public UIController uiController;

    private FirebaseAuth auth;
    private FirebaseUser user;

    private void Awake()
    {
        // 구글 플레이 버전이 맞는지 확인
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if(task.Result == Firebase.DependencyStatus.Available)
            {
                FirebaseInit();
            }
            else
            {
                Debug.LogError("CheckAndFixDependenciesAsync");
            }
        });
    }

    private void FirebaseInit()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChange;       // Auth가 변경되면 이벤트 실행

        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        chatDB.OrderByChild("timestamp").LimitToLast(1).ValueChanged += ReceiveMessage;
    }

    private void AuthStateChange(object sender, EventArgs e)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        if(senderAuth != null)
        {
            user = senderAuth.CurrentUser;          // 현재 접속하고 있는 유저가 누구인지 알 수 있다.
            if(user != null )
            {
                Debug.Log(user.UserId);
                uiController.UpdateUserInfo(true, user.UserId);
            }
        }
    }

    public void SignIn()
    {
        SigninAnonymous();
    }

    public void Signout()
    {
        auth.SignOut();
        uiController.UpdateUserInfo(false);
    }

    private Task SigninAnonymous()
    {
        return auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                Debug.LogError("Signin Fail");
            }
            else if(task.IsCompleted)
            {
                Debug.Log("Signin Completed");
            }
        });
    }

    public void ReadChatMessage()
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        chatDB.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                Debug.LogError("ReadError");
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("ChildCount : " + snapshot.ChildrenCount);
                foreach(var message in snapshot.Children)
                {
                    Debug.Log(message.Key + "  " + message.Child("username").Value.ToString() + "  " + message.Child("message").Value.ToString());
                }
            }
        });
    }

    public void SendChatMessage(string username, string message)
    {
        DatabaseReference chatDB = FirebaseDatabase.DefaultInstance.GetReference("ChatMessage");
        string key = chatDB.Push().Key;

        Dictionary<string, object> msgDic = new Dictionary<string, object>();

        msgDic.Add("username", username);
        msgDic.Add("message", message);
        msgDic.Add("timestamp", ServerValue.Timestamp);
        

        Dictionary<string, object> updateMsg = new Dictionary<string, object>();
        updateMsg.Add(key, msgDic);

        chatDB.UpdateChildrenAsync(updateMsg).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Updata completed");
            }
        });
    }

    List<string> receivedKeyList = new List<string>();

    public void ReceiveMessage(object sender, ValueChangedEventArgs e)
    {
        DataSnapshot snapshot = e.Snapshot;

        Debug.Log("ChildCount : " + snapshot.ChildrenCount);
        foreach (var message in snapshot.Children)
        {
            if(!receivedKeyList.Contains(message.Key))
            {
                string userName = message.Child("username").Value.ToString();
                string msg = message.Child("message").Value.ToString();

                uiController.AddChatMessage(userName, msg);
                receivedKeyList.Add(message.Key);
            }
        }
    }
}
