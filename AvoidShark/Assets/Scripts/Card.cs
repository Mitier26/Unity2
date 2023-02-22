using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasClicked;

    [SerializeField]
    private int row, col;
    // ���۾����� �Է��ߴ�.

    [SerializeField]
    private Sprite gold, fish, shark, unrevealed;   // �̰���

    private SpriteRenderer renderer;
    private Animator animator;

    public Choice myChoice;

    private void Start()
    {
        hasClicked = false;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // �ʱ�ȭ���� Board�� �ִ� ���� ����ȴ�.
        // Board�� ���õǾ� �ִ� Choice�� ������ �´�.
        myChoice = GameManager.instance.myboard.GetChoice(row, col);
        renderer.sprite = unrevealed;
    }

    public void PlayTurn()
    {
        // ī�带 �������� �� ���� �Ǵ� ��
        animator.Play("Reveal");

        hasClicked = true;
    }

    public void ChangeImage()
    {
        // ó������ ��� �ִ� �׸����� ��ġ�Ǿ� �ִٰ�
        // ������ �ϸ� myChoice�� �ִ� ���� ���ؼ� �׸��� �����Ѵ�.
        Sprite current = myChoice == Choice.Fish ? fish : myChoice == Choice.Gold ? gold : shark;

        renderer.sprite = current;
    }
}
