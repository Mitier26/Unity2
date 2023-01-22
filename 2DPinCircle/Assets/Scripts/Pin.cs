using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private GameObject square;     // 핀의 막대기

    public void SetInPinStuckToTarget()
    {
        // 핀의 막대기 활성황
        square.SetActive(true);
    }
}
