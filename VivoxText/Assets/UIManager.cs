using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;

public class UIManager : MonoBehaviour
{
    public vivoxManager vivoxManager;

    public Text userName;
    public Text channelName;
    public Text message;

    public GameObject item;
    public Transform userPos;
    public Transform chatPos;

    public Scrollbar userScrollber;
    public Scrollbar chatScrollber;

    public void LoginBtn()
    {
        vivoxManager.Login(userName.text);
    }

    public void LogoutBtn() 
    {
        vivoxManager.vivox.loginSession.Logout();
    } 

    public void JoinChannerBtn()
    {
        vivoxManager.JoinChannel(channelName.text, ChannelType.NonPositional);
    }

    public void LeaveChannelBtn()
    {
        vivoxManager.vivox.channelSession.Disconnect();
        vivoxManager.vivox.loginSession.DeleteChannelSession(new ChannelId(vivoxManager.vivox.issuer, channelName.text, vivoxManager.vivox.domain, ChannelType.NonPositional));
    }

    public void InputChat(string str)
    {
        var temp = Instantiate(item, chatPos);
        temp.GetComponentInChildren<Text>().text = str;
    }

    public void InputUser(string str)
    {
        var temp = Instantiate(item, userPos);
        temp.GetComponentInChildren<Text>().text = str;
    }

    public void MessageBtn()
    {
        vivoxManager.SendMessage(message.text);
    }
}
