using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Utils 
{
    // 숫자는 1 ~ 45 고정

    private static int[] numbers = new int[45];
    private  static void InitNumbers()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }
    }
    public static int[] SuffleRandom()
    {
        InitNumbers();

        for(int i = 0; i < 10000; i++)
        {
            int rand1 = Random.Range(0, numbers.Length);
            int rand2 = Random.Range(0, numbers.Length);

            int temp = numbers[rand1];
            numbers[rand1] = numbers[rand2];
            numbers[rand2] = temp;
        }

        return numbers;
    }

    public static int[] normalRandom()
    {
        InitNumbers();

        int[] result = new int[6];

        for(int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, 45 - i);
            result[i] = numbers[rand];
            numbers[rand] = numbers[44 - i];
        }

        return result;
    }

    public static int[] SeedRandom(int seed)
    {
        // system.Random 을 사용한다.
        // seed 에 따라 고정된 랜덤값이 필요한 경우 사용한다.
        // 만약, 생년월일을 이용하여 번호를 알려주는 것을 만들 었을때
        // 같은 값을 입력하는데 계속 다른 값이 나오면 사람들이 의심을 할 것이다.
        // AI, ML 의 속임수를 위해 사람들에게 믿음을 심어줄 필요가 있다.

        // 기본적인 사용 방법
        System.Random r = new System.Random(seed);
        int a = r.Next(0, 45);
        return null;
    }
}
