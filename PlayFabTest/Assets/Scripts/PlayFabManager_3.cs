using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayFabManager_3 : MonoBehaviour
{
    public TMP_Text logText;
    public string userID;

    public void SetData()
    {
        // Ű�� ������ �̷���� ��ųʸ�
        // Firebase�� ����.
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => logText.text = "������ ���� ����", (error) => logText.text = "������ ���� ����");
    }

    public void GetData()
    {
        var request = new GetUserDataRequest() { PlayFabId = userID  };
        PlayFabClientAPI.GetUserData(request, (result) => { foreach (var eachData in result.Data) logText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; },
            (error) => print("������ �ҷ����� ����"));
        //PlayFabClientAPI.GetUserData(request, (result) => print(result.Data["A"].Value),
        //    (error) => print("������ �ҷ����� ����"));
    }
}
