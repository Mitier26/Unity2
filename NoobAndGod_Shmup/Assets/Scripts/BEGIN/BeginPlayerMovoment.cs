using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPlayerMovoment : MonoBehaviour
{
    [SerializeField] private float playerLimit = 3;
    [SerializeField] private float playerMoveSpeed = 5;
    

    private void Update()
    {
        float InputX = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.right * InputX * playerMoveSpeed * Time.deltaTime);

        if (transform.position.x < -playerLimit)
        {
            transform.position = new Vector2(-playerLimit, transform.position.y);
        }
        else if(transform.position.x > playerLimit)
        {
            transform.position = new Vector2(playerLimit, transform.position.y);
        }

    }
}
