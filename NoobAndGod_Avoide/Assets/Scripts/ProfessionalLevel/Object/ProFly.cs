using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProFly : ProObject
{
    public float startPositionX;
    public Vector2 direction;

    public void SetFly()
    {
        transform.localPosition = new Vector3(0, transform.position.y, 0);
        GetComponent<Rigidbody2D>().velocity = direction;
    }

}
