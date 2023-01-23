using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.zero;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        // UI �� �Ѿƴٴ� ��� ����
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // Ÿ���� ������ ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // ������Ʈ�� ��ġ�� ���ŵ� ���� UI�� ��ġ�� �Բ� �ϱ� ����

        // ������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡���� ��ǥ ���� ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        // ȭ�鳻���� ��ǥ + �Ÿ� ��ŭ ������ ��ġ�� UI ��ġ�� ����
        rectTransform.position = screenPosition + distance;
    }
}
