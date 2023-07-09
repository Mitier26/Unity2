using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrag : MonoBehaviour
{
    public Vector2 mousePos;

    private void Update()
    {
        mousePos = Input.mousePosition;
    }

}
