using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // 1. 색상을 배열로 등록해두고 사용
    // 2. Random.ColorHSV() 등의 메소드를 잉용해 임의의 색상 사용
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
        // 블록의 색이 변경될 때 마다 정답 블록의 색상이 다른 블록들과 더 비슷한 색상으로 보이도록
        difficultyModifier *= 0.92f;

        // 기본 색상
        currentColor = colorPalette[Random.Range(0, colorPalette.Length)];

        // 정답 블록의 색상
        float diff = (1.0f / 255.0f) * difficultyModifier;
        otherOneColor = new Color(currentColor.r - diff, currentColor.g - diff, currentColor.b - diff);

        // 정답 블록의 번호
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
