using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScenario : MonoBehaviour
{
    [SerializeField] private Progress progress;

    private void Awake()
    {
        SystemSetup();
    }
    
    private void SystemSetup()
    {
        // 활성화되지 않은 상태에서도 게임이 계속 실행
        Application.runInBackground = true;

        // 해성도 설정 
        int width = Screen.width;
        int height = (int)(Screen.width * 18.5f / 9);
        Screen.SetResolution(width, height, true);

        // 화면이 꺼지지 않도록 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 로딩 애니메이션 시작, 재생 완료시 OnAgterPrgress() 메소드 실행
        progress.Play(OnAfterProgress);
    }

    private void OnAfterProgress()
    {

    }
}
