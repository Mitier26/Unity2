using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobTarget : MonoBehaviour
{
    // x = 8, y = 4

    float size = 9f;

    private void Start()
    {
        Reposition();
    }

    public void Reposition()
    {
        transform.localScale = Vector3.one * size;

        float x, y;

        x = Random.Range(-8f, 8f);
        y = Random.Range(-4f, 4f);

        transform.position = new Vector2(x, y);
    }

    public void SetScale(int level)
    {
        // 0.5 ~ 9
        size = Mathf.Lerp(9f, 0.5f, (float)level / 20);
    }
}
