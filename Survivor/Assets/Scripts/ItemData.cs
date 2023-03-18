using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // 아이템이 무기인지 1회용 아이템인지 구분하기 위한 것
    // 근접, 원거리, 글러브, 신발, 음료
    public enum ItemType {  Melee, Range, Glove, Shoe, Heal }

    [Header("# MainInfo")]
    public ItemType itemType;       // 아이템의 타입
    public int itemId;              // 아이템 id
    public string itemName;         // 아이템의 이름
    public string itemDesc;         // 아이템의 설명
    public Sprite itemIcon;         // 아이템의 아이콘 ( 그림 )

    [Header("# Level Data")]
    public float baseDamage;        // 0레벨, 기본적인 공격력
    public int baseCount;           // 0레벨, 기본적인 공격 횟수, 관통
    public float[] damages;          // 레벨업시 공격력
    public int[] counts;            // 레벨업시 공격횟수

    [Header("# Weapon")]
    public GameObject projectile;   // 발사체
    public Sprite hand;

}
