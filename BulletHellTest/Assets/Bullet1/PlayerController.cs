using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float walkSpeed = 5f;

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        transform.Translate(new Vector3(inputX, inputY, 0) * Time.deltaTime * walkSpeed);    
    }
}
