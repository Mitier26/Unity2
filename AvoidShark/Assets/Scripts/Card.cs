using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasClicked;

    [SerializeField]
    private int row, col;
    // 수작업으로 입력했다.

    [SerializeField]
    private Sprite gold, fish, shark, unrevealed;   // 미공개

    private SpriteRenderer renderer;
    private Animator animator;

    public Choice myChoice;

    private void Start()
    {
        hasClicked = false;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // 초기화에서 Board에 있는 것이 실행된다.
        // Board에 세팅되어 있는 Choice를 가지고 온다.
        myChoice = GameManager.instance.myboard.GetChoice(row, col);
        renderer.sprite = unrevealed;
    }

    public void PlayTurn()
    {
        // 카드를 선택했을 때 실행 되는 것
        animator.Play("Reveal");

        hasClicked = true;
    }

    public void ChangeImage()
    {
        // 처음에는 비어 있는 그림으로 배치되어 있다가
        // 선택을 하면 myChoice에 있는 것을 비교해서 그림을 변경한다.
        Sprite current = myChoice == Choice.Fish ? fish : myChoice == Choice.Gold ? gold : shark;

        renderer.sprite = current;
    }
}
