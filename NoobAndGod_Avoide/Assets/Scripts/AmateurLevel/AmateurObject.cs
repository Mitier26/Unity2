using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurObject : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(transform.position.y < -6f)
        {
            gameObject.SetActive(false);
        }
    }
}
