using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로젝트 창에서 ScriptableObject 를 만들리 위한 것.
// fileName 는 생성했을 때 표시되는 이름
// menuName 는 메뉴의 경로, 만들는 것이 많을 때 사용하면 좋다.
[CreateAssetMenu(fileName = "New Data", menuName = "Items")]
public class StageData : ScriptableObject
{
    // 위부에서 값을 변경하지 못 하게하기위한 프로퍼티
    [SerializeField]
    private Vector2 limitMin;
    [SerializeField]
    private Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
