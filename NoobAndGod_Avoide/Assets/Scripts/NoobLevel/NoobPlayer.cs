using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobPlayer : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 1f;

    [SerializeField]
    private ParticleSystem dieParticle;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    public void LeftButtoon()
    {
        transform.position = new Vector3(transform.position.x - moveDistance, transform.position.y);

        if(transform.position.x < -2)
        {
            transform.position = new Vector3(-2, transform.position.y);
        }
    }

    public void RightButton()
    {
        transform.position = new Vector3(transform.position.x + moveDistance, transform.position.y);
        
        if (transform.position.x > 2)
        {
            transform.position = new Vector3(2, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<NoobGameManager>().GameOver();
        spriteRenderer.enabled = false;
        dieParticle.transform.position = transform.position;
        dieParticle.Play();
    }
}
