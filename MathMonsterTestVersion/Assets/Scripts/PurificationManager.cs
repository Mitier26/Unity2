using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurificationManager : MonoBehaviour
{
    public int dirtyEnergy = 1000;
    public int pureEnergy = 0;

    public float purificationTime;
    public float elapsedTime;

    public Slider slider;
    public TextMeshProUGUI dirtyCount;
    public TextMeshProUGUI pureCount;

    private IEnumerator Start()
    {
        while (dirtyEnergy > 0)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > purificationTime)
            {
                dirtyEnergy--;
                pureEnergy++;
                dirtyCount.text = dirtyEnergy.ToString();
                pureCount.text = pureEnergy.ToString();
                elapsedTime = 0f;
            }

            slider.value = elapsedTime / purificationTime;
            
            yield return null;
        }
    }

    public void Purify()
    {
        dirtyEnergy -= 50;
        pureEnergy += 50;
        dirtyCount.text = dirtyEnergy.ToString();
        pureCount.text = pureEnergy.ToString();
    }
}
