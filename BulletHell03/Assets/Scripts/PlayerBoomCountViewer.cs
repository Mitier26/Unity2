using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private TMP_Text textBoomCount;

    private void Awake()
    {
        textBoomCount = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textBoomCount.text = "x " + weapon.BoomCount;
    }
}
