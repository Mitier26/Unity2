using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayFabManager_5 : MonoBehaviour
{
    private void StartCloudScript()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "helloWorld",
            FunctionParameter = new { text = "ㅎㅇㅎㅇ" },
            // 서버에 있는 text 라는 인자에 값을 넣는 것이다.
            // 만약 서버에 있는 인자가 아니면 에어를 뱉는다 = 치터
            // text 를 알아볼수 없는 것으로 만들면 핵 방지가 된다.
            GeneratePlayStreamEvent = true          // 지도 있는것에 표시 되도록 하는 것
        }, OnCloudHelloWorld, (error) => print("실패"));
    }

    [ContextMenu("인시 저장")]
    private void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        jsonResult.TryGetValue("messageValue", out object messageValue);
        Debug.Log((string)messageValue);
    }

    [ContextMenu("클라우드 스크립트 데이터 저장")]
    private void StartSetDate()
    {
        PlayFabClientAPI.ExecuteCloudScript(
        new ExecuteCloudScriptRequest
        {
            FunctionName = "SetDate",
            FunctionParameter = new { OnlineState = true },
            GeneratePlayStreamEvent = true
        }, null, null);
    }
}
