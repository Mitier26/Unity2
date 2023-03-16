using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // �⺻ ����
        name = "Gear " + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        // ���� ����
        type = data.itemType;
        rate = data.damages[0];

        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    // ������ ���� Ÿ�Կ� ���� ��� ������ �ɷ��� ��� ��Ų��.
    private void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // ����� ���
    private void RateUp()
    {
        // �ش� ������Ʈ�� ������ �� �θ� Player �̱� ������ 
        // �÷��̾�� �ö� �� weapon�� ã�´�.
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        // ������� ���� ã�Ҵ�.
        // ã�� ������ �ӵ��� ���� ��� ���Ѿ� �ϱ� ������
        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:     // ���� ����
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:    // ���Ÿ� ����
                    weapon.speed = 0.5f * (1f - rate);
                    // ī��Ʈ�� ������ ���� 0.75 �̴� 1���� 0.75�� ���� ���� �۾����Ƿ� �߻� �ӵ��� ����Ѵ�.
                    break;
            }
        }
    }

    private void SpeedUp()
    {
        float speed = 3;
        GameManager.Instance.player.moveSpeed = speed + speed * rate;
    }
}
