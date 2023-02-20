using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;       // �񺹽��� ����ϱ� ���ؼ� �ʿ��� ��
using System;           // Uri�� ����ϱ� ���ؼ� �ʿ��� ��
using UnityEngine.Android;

[Serializable]
public class Vivox
{
    public Client client;   // �񺹽� ������ ������ �� �ʿ��� ��.

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
        // ����ũ ���� ��û
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        //Login();        // ü�ο� ����
        ui.InputChat("���� ����");
    }

    public void UserCallbacks(bool bind, IChannelSession session)
    {
        if(bind)
        {
            // �ش� ü�ο� �����ڰ� �߰��Ǹ� AddUser�� �����Ѵ�.
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
        ui.InputChat($"{user.Account.Name} ���� ���� �߽��ϴ�.");
        ui.InputUser(user.Account.Name);
    }

    public void LeaveUser(object sender, KeyEventArg<string> userData)
    {
        var temp = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = temp[userData.Key];
        ui.InputChat($"{user.Account.Name} ���� ä���� �������ϴ�.");
    }

    // �񺹽��� �α���

    public void Login(string userName)
    {
        AccountId accountId = new AccountId(vivox.issuer, userName, vivox.domain);      // ���� �����
        vivox.loginSession = vivox.client.GetLoginSession(accountId);                   // �α��� ���� �����
        vivox.loginSession.BeginLogin(vivox.server, vivox.loginSession.GetLoginToken(vivox.tokenKey, vivox.timeSpan),
            callback =>
            {
                try
                {
                    vivox.loginSession.EndLogin(callback);
                    ui.InputChat("�α��� �Ϸ�");
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
    }

    // ä�ο� ����

    public void JoinChannel(string channelName, ChannelType channelType)
    {
        ChannelId channelId = new ChannelId(vivox.issuer, channelName, vivox.domain, channelType);
        vivox.channelSession = vivox.loginSession.GetChannelSession(channelId);
        // �˸�
        UserCallbacks(true, vivox.channelSession);
        ChannelCallbacks(true, vivox.channelSession);

        vivox.channelSession.BeginConnect(true, true, true, 
            vivox.channelSession.GetConnectToken(vivox.tokenKey, vivox.timeSpan),
            callback =>
            {
                try
                {
                    vivox.channelSession.EndConnect(callback);  // ü�ο� ����
                    ui.InputChat("ü�� ���� �Ϸ�");
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
        // �޼����� ���� ����� �̸�
        var name = itemAddedEventArgs.Value.Sender.Name;
        var message = itemAddedEventArgs.Value.Message;

        ui.InputChat(name + " : " + message);
    }
}
