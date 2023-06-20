using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private AudioSource sound;

    private Vector3 startPosition;
    private Vector3 oldPosition;
    private bool isTurn = false;

    private int moveCnt = 0;
    private int turnCnt = 0;
    private int spawnCnt = 0;

    private bool isDie = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        startPosition = transform.position;
        Init();
    }

    private void Init()
    {
        anim.SetBool("Die", false);
        transform.position = startPosition;
        oldPosition = startPosition;

        moveCnt = 0;
        spawnCnt = 0;
        turnCnt = 0;
        isTurn = false;
        spriteRenderer.flipX = isTurn;
        isDie = false;
    }

    public void CharTurn()
    {
        isTurn = isTurn == true ? false : true;
        spriteRenderer.flipX = isTurn;
    }

    public void CharMove()
    {
        if (isDie) return;

        sound.Play();

        moveCnt++;

        MoveDirection();

        if(IsFailTurn())
        {
            CharDie();
            return;
        }

        if(moveCnt > 5)
        {
            RespawnStair();
        }

        GameManager.Instance.AddScore();
    }

    private void MoveDirection()
    {
        if (isTurn)
        {
            oldPosition += new Vector3(-0.75f, 0.5f, 0);
        }
        else
        {
            oldPosition += new Vector3(0.75f, 0.5f, 0);
        }

        transform.position = oldPosition;
        anim.SetTrigger("Move");
    }

    private bool IsFailTurn()
    {
        bool result = false;

        if (GameManager.Instance.isTurn[turnCnt] != isTurn)
        {
            result = true;
        }

        turnCnt++;

        if(turnCnt > GameManager.Instance.stairs.Length - 1)
        {
            turnCnt = 0;
        }

        return result;
    }

    private void RespawnStair()
    {
        GameManager.Instance.SpawnStair(spawnCnt);
        spawnCnt++;

        if(spawnCnt > GameManager.Instance.stairs.Length - 1)
        {
            spawnCnt = 0;
        }
    }

    private void CharDie()
    {
        GameManager.Instance.GameOver();
        anim.SetBool("Die", true);
        isDie = true;
    }

    public void ButtonRestart()
    {
        Init();
        GameManager.Instance.Init();
        GameManager.Instance.InitStatirs();
    }
}
