using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByPosition : MonoBehaviour
{
    private float destroyWeigth = 2;

    private void LateUpdate()
    {
        if( transform.position.x < Constants.min.x - destroyWeigth ||
            transform.position.x > Constants.max.x + destroyWeigth ||
            transform.position.y < Constants.min.y - destroyWeigth ||
            transform.position.y > Constants.max.y + destroyWeigth)
        {
            Destroy(gameObject);
        }
    }
}
