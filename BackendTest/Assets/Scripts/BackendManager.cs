using UnityEngine;
using BackEnd;          // 뒤끝 SDK

public class BackendManager : MonoBehaviour
{
    private void Awake()
    {
        BackendSetup();
    }

    private void BackendSetup()
    {
        // 뛰 끝 초기화
        var bro = Backend.Initialize();

        // 뒤끝 초기화에 대한 응답값
        if(bro.IsSuccess())
        {
            // 초기화 성공 시 status 204
        }
        else
        {
            // 초기화 실패 tl status 400 대
        }
    }
}
