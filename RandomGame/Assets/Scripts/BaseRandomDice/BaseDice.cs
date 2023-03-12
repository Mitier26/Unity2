using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDice : MonoBehaviour
{
   public static int Roll()
    {
        int dice = Random.Range(1, 7);

        return dice;
    }
}
