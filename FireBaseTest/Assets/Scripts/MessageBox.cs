using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public TMP_Text username;
    public TMP_Text message;

    public void SetMessage(string username, string message)
    {
        this.username.text = username;
        this.message.text = message;
    }
}
