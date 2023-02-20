using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    // 업데이트에 넣는 것보다는 값이 변경될 때 실행하는 것이 좋다.
    private void Update()
    {
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;
    }
}
