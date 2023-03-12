using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDice : MonoBehaviour
{
    public static int Roll()
    {
        int dice;
        int random_probability = Random.Range(0, 100);

        if(random_probability < 35)
        {
            dice = 6;
        }
        else
        {
            dice = Random.Range(1, 6);
        }

        return dice;
    }
}
