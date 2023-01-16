using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // 1. ������ �迭�� ����صΰ� ���
    // 2. Random.ColorHSV() ���� �޼ҵ带 �׿��� ������ ���� ���
    [SerializeField] private Color[] colorPalette;
    [SerializeField] private float difficultyModifier;
    [SerializeField][Range(2, 5)] private int blockCount = 2;
    [SerializeField] private BlockSpawner blockSpawner;

    private List<Block> blockList = new List<Block>();

    private Color currentColor;
    private Color otherOneColor;

    private int otherBlockIndex;

    private void Awake()
    {
        blockList = blockSpawner.SpawnBlock(blockCount);

        for(int i = 0; i < blockList.Count; i++)
        {
            blockList[i].Setup(this);
        }

        Setcolor();
    }

    //private void Start()
    //{
    //    Setcolor();
    //}

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Setcolor();
    //    }
    //}

    private void Setcolor()
    {
        // ����� ���� ����� �� ���� ���� ����� ������ �ٸ� ��ϵ�� �� ����� �������� ���̵���
        difficultyModifier *= 0.92f;

        // �⺻ ����
        currentColor = colorPalette[Random.Range(0, colorPalette.Length)];

        // ���� ����� ����
        float diff = (1.0f / 255.0f) * difficultyModifier;
        otherOneColor = new Color(currentColor.r - diff, currentColor.g - diff, currentColor.b - diff);

        // ���� ����� ��ȣ
        otherBlockIndex = Random.Range(0, blockList.Count);

        for(int i = 0; i < blockList.Count; i++)
        {
            if(i == otherBlockIndex)
            {
                blockList[i].Color = otherOneColor;
            }
            else
            {
                blockList[i].Color = currentColor;
            }
            
        }
    }

    private void SetColor2()
    {
        Color color = Random.ColorHSV();
        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].Color = color;
        }
    }

    public void CheckBlock(Color color)
    {
        if (blockList[otherBlockIndex].Color == color)
        {
            Setcolor();
        }
        else
        {
            UnityEditor.EditorApplication.ExitPlaymode();
        }
    }
}
