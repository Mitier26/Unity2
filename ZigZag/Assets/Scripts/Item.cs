using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemGetEffectPrefab;
    private GameController gameController;
    private float rotateSpeed;

    public void Setup(GameController gameController)
    {
        this.gameController = gameController;

        itemGetEffectPrefab = Instantiate(itemGetEffectPrefab, transform.position, Quaternion.identity);
        itemGetEffectPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        rotateSpeed = Random.Range(10, 100);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 0) * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            itemGetEffectPrefab.transform.position = transform.position;
            itemGetEffectPrefab.SetActive(true);

            gameController.IncreaseScore(5);
            gameObject.SetActive(false);

        }
    }
}
