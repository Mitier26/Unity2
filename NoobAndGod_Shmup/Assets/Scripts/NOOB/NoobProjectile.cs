using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private bool isPlayer;
    [SerializeField] private float destroyTime = 5f;
    
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void SetProjectile(Vector2 direction, float moveSpeed)
    {
        this.direction = direction;
        this.moveSpeed = moveSpeed;
    }

    public void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

}
