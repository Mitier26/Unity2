using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;                // �ܻ� ȿ���� ����
    public float ghostDelaySeconds;         // �ܻ� ���� ������ ��� �ð�
    public GameObject ghost;                // �ܻ� ������
    public bool makeGhost = false;          // �ܻ��� ON, OFF

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if(makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                // �ܻ� ȿ���� ����� ������Ʈ�� �����Ѵ�. ( ������Ʈ Ǯ�� ����� ���� ���� )
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                // �ڽ��� ���� ������ �׸�, ���ϸ��̼��� ���� ���
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                // ������� �ܻ� ������Ʈ�� ������ �θ��� ����� ���� �Ѵ�. ��, ��
                currentGhost.transform.localScale = this.transform.localScale;
                // ������� �ܻ� ������Ʈ�� �׸��� ���� �÷��̾��� �׸����� �Ѵ�.
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                // 1���� �ı�
                Destroy(currentGhost, 1f);
            }
        }
        
    }
}
