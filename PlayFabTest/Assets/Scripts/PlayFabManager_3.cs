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
        // 키와 값으로 이루어진 딕셔너리
        // Firebase랑 같다.
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => logText.text = "데이터 저장 성공", (error) => logText.text = "데이터 저장 실패");
    }

    public void GetData()
    {
        var request = new GetUserDataRequest() { PlayFabId = userID  };
        PlayFabClientAPI.GetUserData(request, (result) => { foreach (var eachData in result.Data) logText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; },
            (error) => print("데이터 불러오기 실패"));
        //PlayFabClientAPI.GetUserData(request, (result) => print(result.Data["A"].Value),
        //    (error) => print("데이터 불러오기 실패"));
    }
}
