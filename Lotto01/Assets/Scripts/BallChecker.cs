using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI.ProceduralImage;

public class BallChecker : MonoBehaviour
{
    public GameObject selectedBallPrefab;
    public Transform parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Selected"))
        {
            GameObject selectedNumber = Instantiate(selectedBallPrefab, parent);

            selectedNumber.GetComponent<ProceduralImage>().color = collision.GetComponent<SpriteRenderer>().color;
            selectedNumber.GetComponentInChildren<TextMeshProUGUI>().text = collision.GetComponent<Ball>().number.ToString();
        }
    }
}
