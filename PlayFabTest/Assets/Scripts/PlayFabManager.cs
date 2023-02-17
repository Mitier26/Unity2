using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    // 이메일로 아이디를 만들고
    // 이메일로 접속하는 방법
    public TMP_InputField emailInput, passwordInput, usernameInput;
    public TMP_Text logText;

    public void LoginButton()
    {
        // 유니티 PlayFab EdEx에 타이틀ID 가 없을 경우 144로 만드는것이다.
        // 스튜디오를 선택했을 때 타이틀이 자동으로 입력 되었으므로 없어도 된다.
        //if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) PlayFabSettings.TitleId = "144";

        // 지정된 ID로 접속
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartGuide", CreateAccount= true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        // 이메일로 접속
        var request = new LoginWithEmailAddressRequest { Email = emailInput.text, Password = passwordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => logText.text = "로그인 성공", (error) => logText.text = "로그인 실패");
    }

    public void RegisterButton()
    {
        // 회원 가입
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = usernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => logText.text = "회원가입 성공", (error) => logText.text = "회원가입 실패");
    }


    public void LogoutButton()
    {
        // 이것은 로그아웃 하는 것이다.
        // 하지만 PlayFab는 상태를 저장하지 않기 때문에 다른 것으로 로그인 하면된다.
        PlayFabClientAPI.ForgetAllCredentials();
    }

    /*
    public void RegisterButton()
    {
        // 요런식으로 만들 수도 있다. 
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = usernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => print("회원가입 성공"), (error) => print("회원가입 실패");
    }
    */

    // 이런식으로도 만들 수 있다.
    private void OnLoginSuccess(LoginResult result) => print("로그인 성공");

    private void OnLoginFailure(PlayFabError error)
    {
        print("로그인 실패");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        print("회원가입 성공");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        print("회원가입 실패");
    }
}
