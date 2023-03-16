using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // 기본 세팅
        name = "Gear " + data.itemId;
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;

        // 변수 세팅
        type = data.itemType;
        rate = data.damages[0];

        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    // 선택한 것의 타입에 따라 모든 무기의 능력을 향상 시킨다.
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

    // 연사력 상승
    private void RateUp()
    {
        // 해당 오브젝트가 생성될 때 부모가 Player 이기 때문에 
        // 플레이어로 올라간 후 weapon을 찾는다.
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        // 무기들을 전부 찾았다.
        // 찾는 무기의 속도를 전부 상승 시켜야 하기 때문에
        foreach(Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:     // 근접 무기
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:    // 원거리 무기
                    weapon.speed = 0.5f * (1f - rate);
                    // 카운트의 마지막 값은 0.75 이다 1에서 0.75를 빼면 값이 작아지므로 발사 속도가 상승한다.
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
