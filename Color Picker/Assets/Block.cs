using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private GameController gameController;
    public Color Color
    {
        set => image.color = value;
        get => image.color;
    }

    public void Setup(GameController gameController)
    {
        image = GetComponent<Image>();
        this.gameController = gameController;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        gameController.CheckBlock(Color);
    }
}
