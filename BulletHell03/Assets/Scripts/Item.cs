using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { PowerUp = 0,  Boom, HP}
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    private Movement2D movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement2D.MoveTo(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Use(GameObject player)
    {
        // 아이템의 타입에 따라 다른 효과를 적용하기 위해
        switch(itemType)
        {
            case ItemType.PowerUp:
                player.GetComponent<Weapon>().AttackLevel++;
                break;
            case ItemType.Boom:
                player.GetComponent<Weapon>().BoomCount++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 2; ;
                break;

        }
    }
}
