using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10f);
    }
}
