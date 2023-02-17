using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public FirebaseController firebase;

    public Button signInButton;
    public Button signOutButton;
    public TMP_Text userIDLabel;

    public TMP_InputField usernameInput;
    public TMP_InputField messageInput;

    public MessageBox messageBoxPrefab;
    public Transform  messageparent;

    private void Start()
    {
        UpdateUserInfo(false);
    }

    public void UpdateUserInfo(bool isSigned, string userID = "")
    {
        if(isSigned)
        {
            signInButton.interactable = false;
            signOutButton.interactable = true;
            userIDLabel.text = "UserID : " + userID;
        }
        else
        {
            signInButton.interactable = true;
            signOutButton.interactable = false;
            userIDLabel.text = "Sign Out....";
        }
    }

    public void SendChat()
    {
        firebase.SendChatMessage(usernameInput.text, messageInput.text);
    }

    public void AddChatMessage(string username, string message)
    {
        MessageBox box = Instantiate(messageBoxPrefab, messageparent);
        box.SetMessage(username, message);
    }
}
