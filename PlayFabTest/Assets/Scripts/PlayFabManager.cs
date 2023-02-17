using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    // �̸��Ϸ� ���̵� �����
    // �̸��Ϸ� �����ϴ� ���
    public TMP_InputField emailInput, passwordInput, usernameInput;
    public TMP_Text logText;

    public void LoginButton()
    {
        // ����Ƽ PlayFab EdEx�� Ÿ��ƲID �� ���� ��� 144�� ����°��̴�.
        // ��Ʃ����� �������� �� Ÿ��Ʋ�� �ڵ����� �Է� �Ǿ����Ƿ� ��� �ȴ�.
        //if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) PlayFabSettings.TitleId = "144";

        // ������ ID�� ����
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartGuide", CreateAccount= true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        // �̸��Ϸ� ����
        var request = new LoginWithEmailAddressRequest { Email = emailInput.text, Password = passwordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => logText.text = "�α��� ����", (error) => logText.text = "�α��� ����");
    }

    public void RegisterButton()
    {
        // ȸ�� ����
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = usernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => logText.text = "ȸ������ ����", (error) => logText.text = "ȸ������ ����");
    }


    public void LogoutButton()
    {
        // �̰��� �α׾ƿ� �ϴ� ���̴�.
        // ������ PlayFab�� ���¸� �������� �ʱ� ������ �ٸ� ������ �α��� �ϸ�ȴ�.
        PlayFabClientAPI.ForgetAllCredentials();
    }

    /*
    public void RegisterButton()
    {
        // �䷱������ ���� ���� �ִ�. 
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = usernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("ȸ������ ����"), (error) => print("ȸ������ ����");
    }
    */

    // �̷������ε� ���� �� �ִ�.
    private void OnLoginSuccess(LoginResult result) => print("�α��� ����");

    private void OnLoginFailure(PlayFabError error)
    {
        print("�α��� ����");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("ȸ������ ����");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        print("ȸ������ ����");
    }
}
