using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 720f;
    public Transform spwnPoint;

    private void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime * Input.GetAxisRaw("Horizontal"), 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            Destroy(other.gameObject);
            GameManager.instance.EndGame();
        }
    }
}
