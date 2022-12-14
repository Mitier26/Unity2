using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image[] ProgressBars;
    public TMP_Text[] ProgressBarsText;

    public void Update()
    {
        ProgressBars[0].fillAmount += 0.5f * Time.deltaTime;
        ProgressBars[1].fillAmount += 0.2f * Time.deltaTime;
        ProgressBars[2].fillAmount += Time.deltaTime;
        ProgressBars[3].fillAmount += 0.1f * Time.deltaTime;
        ProgressBars[4].fillAmount += 0.05f * Time.deltaTime;

        ProgressBarsText[0].text = $"{ProgressBars[0].fillAmount / 1 * 100:F0}%";
        ProgressBarsText[1].text = $"{(1 - ProgressBars[1].fillAmount) * 5:F2}\n Seconds";
        ProgressBarsText[2].text = $"{ProgressBars[2].fillAmount / 1 * 100:F1}%";
        ProgressBarsText[3].text = $"{ProgressBars[3].fillAmount / 1 * 4:F1} / 4";

        foreach (var bar in ProgressBars)
        {
            if (bar.fillAmount >= 1)
                bar.fillAmount = 0;
        }
    }
}
