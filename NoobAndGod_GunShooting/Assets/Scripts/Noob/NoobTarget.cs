using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobTarget : MonoBehaviour
{
    // x = 8, y = 4

    public float maxSize = 9f;
    public float minSize = 0.5f;
    public float currentSize;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        currentSize = maxSize;
        Reposition();
    }

    public void Reposition()
    {
        transform.localScale = Vector3.one * SetScale(NoobGameManager.instance.Combo);

        float x, y;

        x = Random.Range(-8f, 8f);
        y = Random.Range(-4f, 4f);

        transform.position = new Vector2(x, y);
    }

    public float SetScale(int level)
    {
        // 0.5 ~ 9
        return currentSize = Mathf.Lerp(maxSize, minSize, (float)level / NoobGameManager.instance.maxCombo);
    }
}
