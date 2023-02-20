using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;       // 비복스를 사용하기 위해서 필요한 것
using System;           // Uri를 사용하기 위해서 필요한 것
using UnityEngine.Android;

[Serializable]
public class Vivox
{
    public Client client;   // 비복스 서버에 접근할 때 필요한 것.

    public Uri server = new Uri("https://mt1s.www.vivox.com/api2");
    public string issuer = "malkan5121-vi72-dev";
    public string domain = "mt1s.vivox.com";
    public string tokenKey = "exam948";
    public TimeSpan timeSpan = TimeSpan.FromSeconds(90);

    public ILoginSession loginSession;
    public IChannelSession channelSession;
}
public class vivoxManager : MonoBehaviour
{
    public Vivox vivox = new Vivox();

    public UIManager ui;

    private void Awake()
    {
        vivox.client = new Client();
        vivox.client.Uninitialize();
        vivox.client.Initialize();
        DontDestroyOnLoad(gameObject);
    }


    private void OnApplicationQuit()
    {
        vivox.client.Uninitialize();
    }

    private void Start()
    {
        // 마이크 권한 요청
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        //Login();        // 체널에 접속
        ui.InputChat("게임 시작");
    }

    public void UserCallbacks(bool bind, IChannelSession session)
    {
        if(bind)
        {
            // 해당 체널에 참여자가 추가되면 AddUser를 실행한다.
            vivox.channelSession.Participants.AfterKeyAdded += AddUser;
            vivox.channelSession.Participants.BeforeKeyRemoved += LeaveUser;
        }
        else
        {
            vivox.channelSession.Participants.AfterKeyAdded -= AddUser;
            vivox.channelSession.Participants.BeforeKeyRemoved -= LeaveUser;
        }
    }

    public void ChannelCallbacks(bool bind, IChannelSession session)
    {
        if(bind)
        {
            session.MessageLog.AfterItemAdded += ReciveMessage;
        }
        else
        {
            session.MessageLog.AfterItemAdded -= ReciveMessage;
        }
    }

    public void AddUser(object sender, KeyEventArg<string> userData)
    {
        var temp = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = temp[userData.Key];
        ui.InputChat($"{user.Account.Name} 님이 접속 했습니다.");
        ui.InputUser(user.Account.Name);
    }

    public void LeaveUser(object sender, KeyEventArg<string> userData)
    {
        var temp = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = temp[userData.Key];
        ui.InputChat($"{user.Account.Name} 님이 채널의 떠났습니다.");
    }

    // 비복스에 로그인

    public void Login(string userName)
    {
        AccountId accountId = new AccountId(vivox.issuer, userName, vivox.domain);      // 계정 만들기
        vivox.loginSession = vivox.client.GetLoginSession(accountId);                   // 로그인 섹션 만들기
        vivox.loginSession.BeginLogin(vivox.server, vivox.loginSession.GetLoginToken(vivox.tokenKey, vivox.timeSpan),
            callback =>
            {
                try
                {
                    vivox.loginSession.EndLogin(callback);
                    ui.InputChat("로그인 완료");
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
    }

    // 채널에 접속

    public void JoinChannel(string channelName, ChannelType channelType)
    {
        ChannelId channelId = new ChannelId(vivox.issuer, channelName, vivox.domain, channelType);
        vivox.channelSession = vivox.loginSession.GetChannelSession(channelId);
        // 알림
        UserCallbacks(true, vivox.channelSession);
        ChannelCallbacks(true, vivox.channelSession);

        vivox.channelSession.BeginConnect(true, true, true, 
            vivox.channelSession.GetConnectToken(vivox.tokenKey, vivox.timeSpan),
            callback =>
            {
                try
                {
                    vivox.channelSession.EndConnect(callback);  // 체널에 접속
                    ui.InputChat("체널 접속 완료");
                }
                catch(Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
    }

    public void SendMessage(string str)
    {
        vivox.channelSession.BeginSendText(str, callback =>
        {
            try
            {
                vivox.channelSession.EndSendText(callback);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        });
    }

    public void ReciveMessage(object sender, QueueItemAddedEventArgs<IChannelTextMessage> itemAddedEventArgs)
    {
        // 메세지를 보낸 사람의 이름
        var name = itemAddedEventArgs.Value.Sender.Name;
        var message = itemAddedEventArgs.Value.Message;

        ui.InputChat(name + " : " + message);
    }
}
