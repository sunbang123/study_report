using UnityEngine;

public class TranslateVertical : MonoBehaviour
{
    public float speed = 0.1f;

    public void TranslateDown()
    {
        // ������ ����������
        Vector3 position = this.gameObject.transform.position;
        position.y -= 1f * speed;
        this.gameObject.transform.position = position;
    }

    public void TranslateUp()
    {
        // ������ �ö󰡵���
        Vector3 position = this.gameObject.transform.position;
        position.y += 1f * speed;
        this.gameObject.transform.position = position;
    }

    // �ʿ�� �̵� ���߱�
    public void StopMoving()
    {
        CancelInvoke();
    }

    // ���� �̵����� ��ȯ
    public void StartMovingUp()
    {
        StopMoving();
        InvokeRepeating("TranslateUp", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }

    // �Ʒ��� �̵����� ��ȯ
    public void StartMovingDown()
    {
        StopMoving();
        InvokeRepeating("TranslateDown", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }
}
