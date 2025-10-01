using UnityEngine;

public class RotateHorizontal : MonoBehaviour
{
    public float speed = 0.1f;

    public void RotateRight()
    {
        // Z�� ���� �ð���� ȸ��
        this.transform.Rotate(0, 0, -speed);
    }

    public void RotateLeft()
    {
        // Z�� ���� �ݽð���� ȸ��
        this.transform.Rotate(0, 0, speed);
    }

    // �ʿ�� �̵� ���߱�
    public void StopMoving()
    {
        CancelInvoke();
    }

    // ���� �̵����� ��ȯ
    public void StartRotateRight()
    {
        StopMoving();
        InvokeRepeating("RotateRight", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }

    // �Ʒ��� �̵����� ��ȯ
    public void StartRotateLeft()
    {
        StopMoving();
        InvokeRepeating("RotateLeft", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }
}
