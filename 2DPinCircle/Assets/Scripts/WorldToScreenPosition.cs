using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    [SerializeField] private Vector3 distance = Vector3.zero;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        // UI 가 쫓아다닐 대상 설정
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // 타켓이 없으면 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 오브젝트의 위치가 갱신된 이후 UI도 위치를 함께 하기 위해

        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

        // 화면내에서 좌표 + 거리 만큼 떨어진 위치를 UI 위치로 설정
        rectTransform.position = screenPosition + distance;
    }
}
