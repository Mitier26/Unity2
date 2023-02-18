using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;                        // �������� �����͸� ������ ���� ���Ѱ�
    private Movement2D movement2D;                      // �̵��ϱ�

    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;      // ����Ű ����
    private Weapon weapon;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon= GetComponent<Weapon>();
    }

    private void Update()
    {
        // �̵��Ϸ��� �ϴ� ������ �������ش�.
        // GetAxisRaw�� �̿��� �����¿츦 �޴´�.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // movement2D�� ���Ⱚ�� ���� �ش�.
        movement2D.MoveTo(new Vector3(x, y, 0));

        // ��ư�� �������� ����
        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if(Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }

    }

    private void LateUpdate()
    {
        // �÷��̾ ȭ�� ������ ������ ���ϱ� ���� ��
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
            Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y), 0);
    }
}
