using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmateurBullet : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

    private void OnEnable()
    {
        Invoke(nameof(DisableBullet), 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetBullet(Vector2 direction, float speed,  Quaternion rotation)
    {
        this.direction = direction;
        this.speed = speed;
        transform.rotation = rotation;
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
