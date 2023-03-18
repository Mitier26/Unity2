using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // �������� �������� 1ȸ�� ���������� �����ϱ� ���� ��
    // ����, ���Ÿ�, �۷���, �Ź�, ����
    public enum ItemType {  Melee, Range, Glove, Shoe, Heal }

    [Header("# MainInfo")]
    public ItemType itemType;       // �������� Ÿ��
    public int itemId;              // ������ id
    public string itemName;         // �������� �̸�
    public string itemDesc;         // �������� ����
    public Sprite itemIcon;         // �������� ������ ( �׸� )

    [Header("# Level Data")]
    public float baseDamage;        // 0����, �⺻���� ���ݷ�
    public int baseCount;           // 0����, �⺻���� ���� Ƚ��, ����
    public float[] damages;          // �������� ���ݷ�
    public int[] counts;            // �������� ����Ƚ��

    [Header("# Weapon")]
    public GameObject projectile;   // �߻�ü
    public Sprite hand;

}
