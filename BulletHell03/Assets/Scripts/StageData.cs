using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ â���� ScriptableObject �� ���鸮 ���� ��.
// fileName �� �������� �� ǥ�õǴ� �̸�
// menuName �� �޴��� ���, ����� ���� ���� �� ����ϸ� ����.
[CreateAssetMenu(fileName = "New Data", menuName = "Items")]
public class StageData : ScriptableObject
{
    // ���ο��� ���� �������� �� �ϰ��ϱ����� ������Ƽ
    [SerializeField]
    private Vector2 limitMin;
    [SerializeField]
    private Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
