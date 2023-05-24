using UnityEngine;
using BackEnd;          // �ڳ� SDK

public class BackendManager : MonoBehaviour
{
    private void Awake()
    {
        BackendSetup();
    }

    private void BackendSetup()
    {
        // �� �� �ʱ�ȭ
        var bro = Backend.Initialize();

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if(bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� status 204
        }
        else
        {
            // �ʱ�ȭ ���� tl status 400 ��
        }
    }
}
