using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private GameObject brickPrefab;

    [SerializeField]
    private Gradient gradient;

    private void Awake()
    {
        for(int i = 0;  i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                GameObject brick = Instantiate(brickPrefab, transform);
                //brick.transform.position = transform.position + new Vector3(i * offset.x, j* offset.y, 0);
                brick.transform.position = transform.position + new Vector3((float)((size.x-1)*0.5f-i) * offset.x, j * offset.y, 0);
                brick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j / (size.y - 1));
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
