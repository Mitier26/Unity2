using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    // Button���� �ص� ����ϰ� �۵��մϴ�.
    public Toggle toggle;
    // Ŭ�� ���� �� ������ ��
    public Color pressedColor;
    // ��ư�� �⺻���� ���� �����Ѵ�.
    public ColorBlock originColor;

    private void Start()
    {
        originColor = toggle.colors;
    }

    // ��ư�� Ŭ�� ���� �� �۵�
    public void PressToggle(Toggle toggle)
    {
        if(toggle.isOn)
        {
            ColorBlock color = toggle.colors;
            color.normalColor = pressedColor;
            color.pressedColor = pressedColor;
            color.selectedColor = pressedColor;
            toggle.colors = color;
        }
        else
        {
            toggle.colors = originColor;
        }
    }

}
