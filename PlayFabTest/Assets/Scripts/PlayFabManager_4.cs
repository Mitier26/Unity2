using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabManager_4 : MonoBehaviour
{
    public TMP_InputField emailInput, passwordInput, usernameInput;
    public TMP_Text logText;
    public Image[] flagImage;

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = emailInput.text, Password = passwordInput.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => print("�α��� ����"), (error) => print("�α��� ����"));
    }

    public void RegisterButton()
    {
        // �����ϴ� ������ �г����� �Է��ϴ� ���� ���� ����.
        var request = new RegisterPlayFabUserRequest { Email = emailInput.text, Password = passwordInput.text, Username = usernameInput.text, DisplayName = usernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => logText.text = "ȸ������ ����", (error) => logText.text = "ȸ������ ����");
    }

    public void SetStat()
    {
        var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "HighScore", Value = 50 } } };
        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => print("�� �����"), (error) => print("�� �������"));
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "HighScore", MaxResultsCount = 20, ProfileConstraints = new PlayerProfileViewConstraints() { ShowLocations = true, ShowDisplayName = true } };
        PlayFabClientAPI.GetLeaderboard(request, (result) =>
        {
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var curBoard = result.Leaderboard[i];
                logText.text += curBoard.Profile.Locations[0].CountryCode.Value + " / " + curBoard.DisplayName + " / " + curBoard.StatValue + "\n";

                // ���� ǥ��
                flagImage[i].gameObject.SetActive(true);
                flagImage[i].sprite = Resources.Load<Sprite>(curBoard.Profile.Locations[0].CountryCode.Value.ToString().ToLower());
            }
        },
        (error) => print("�������� �ҷ����� ����"));
    }
}
