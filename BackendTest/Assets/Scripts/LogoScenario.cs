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
        // Ȱ��ȭ���� ���� ���¿����� ������ ��� ����
        Application.runInBackground = true;

        // �ؼ��� ���� 
        int width = Screen.width;
        int height = (int)(Screen.width * 18.5f / 9);
        Screen.SetResolution(width, height, true);

        // ȭ���� ������ �ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // �ε� �ִϸ��̼� ����, ��� �Ϸ�� OnAgterPrgress() �޼ҵ� ����
        progress.Play(OnAfterProgress);
    }

    private void OnAfterProgress()
    {

    }
}
