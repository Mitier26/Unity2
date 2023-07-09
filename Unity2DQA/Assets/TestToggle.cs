using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestToggle : MonoBehaviour
{
    // Button으로 해도 비슷하게 작동합니다.
    public Toggle toggle;
    // 클릭 했을 때 변경할 색
    public Color pressedColor;
    // 버튼은 기본색을 따로 저장한다.
    public ColorBlock originColor;

    private void Start()
    {
        originColor = toggle.colors;
    }

    // 버튼을 클릭 했을 때 작동
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
