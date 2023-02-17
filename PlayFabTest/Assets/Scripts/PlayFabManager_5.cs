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
            FunctionParameter = new { text = "��������" },
            // ������ �ִ� text ��� ���ڿ� ���� �ִ� ���̴�.
            // ���� ������ �ִ� ���ڰ� �ƴϸ� ��� ��´� = ġ��
            // text �� �˾ƺ��� ���� ������ ����� �� ������ �ȴ�.
            GeneratePlayStreamEvent = true          // ���� �ִ°Ϳ� ǥ�� �ǵ��� �ϴ� ��
        }, OnCloudHelloWorld, (error) => print("����"));
    }

    [ContextMenu("�ν� ����")]
    private void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        jsonResult.TryGetValue("messageValue", out object messageValue);
        Debug.Log((string)messageValue);
    }

    [ContextMenu("Ŭ���� ��ũ��Ʈ ������ ����")]
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
