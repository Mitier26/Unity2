using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChaser : MonoBehaviour
{

    Vector3 mousePos;
    Vector3 pos;

    private void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = mousePos.z - Camera.main.transform.localPosition.z;

        pos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
    }
}
