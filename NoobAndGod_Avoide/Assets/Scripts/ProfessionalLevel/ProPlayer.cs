using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;          // �ٴ� Ȯ���� ���� ũ�� ���
    public FixedJoystick joystick;

    [SerializeField]
    private float moveSpeed = 5f;                       // �̵� �ӵ�
    [SerializeField]
    private float jumpForce = 14f;                      // ���� �Ŀ�

    private float horizontalInput;                      // �¿� �Է� ��
    private Vector3 moveDirection = Vector3.zero;       // �̵� ����

    [SerializeField]
    private bool isGround;                              // �ٴڿ� �ִ��� Ȯ��
    [SerializeField]
    private LayerMask groundLayer;                      // �ٴ� Ȯ���� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (!ProManager.instance.isString) return;

        horizontalInput = joystick.Horizontal;
        //horizontalInput = Input.GetAxisRaw("Horizontal");

        HorizontalMovement();

        //if (Input.GetKeyDown(KeyCode.Space) && isGround)
        //{
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //    isGround = false;
        //}
    }

    public void Jump()
    {
        if (!isGround) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        isGround = false;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider.size.y, groundLayer);
        if (hit.collider != null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));   
    }

    private void HorizontalMovement()
    {
        moveDirection = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;

        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        transform.Translate(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            collision.GetComponent<ProObject>().DestroyObject();
        }
        if (collision.CompareTag("Fish"))
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (collision.CompareTag("Ball"))
        {
            collision.gameObject.transform.parent.GetComponent<ProObject>().DestroyObject();
        }

        if(!collision.CompareTag("Water") && !collision.CompareTag("Detector"))
        {
            ProAudioManager.instance.PlaySound(ProAudioManager.PROSFX.Death, transform.position);
            ProManager.instance.Gameover();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            collision.gameObject.GetComponent<ProObject>().DestroyObject();

            ProAudioManager.instance.PlaySound(ProAudioManager.PROSFX.Get, transform.position);
            ProParticleManager.instance.PlayParticle(ProParticleManager.PARTICLE.Get, collision.contacts[0].point);
        }
    }
}
