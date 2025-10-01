using UnityEngine;
public class Forever_ChaseCameraH : MonoBehaviour
{
    Vector3 base_pos;
    public bool isEnabled = false;
    public static Forever_ChaseCameraH instance;
    private void Start()
    {
        instance = this.GetComponent<Forever_ChaseCameraH>();
        base_pos = Camera.main.gameObject.transform.position;
    }

    private void LateUpdate() // �÷��̾ �����̰� ī�޶� �������ߵǴϱ�
    {
        if (!isEnabled) return; // ��Ȱ��ȭ �� ���� �� ��

        if (this.transform.transform.position.x < -7 || this.transform.transform.position.x > 6.5)
        {
            return;
        }
        Vector3 pos = this.transform.position;
        Camera.main.gameObject.transform.position = new Vector3(pos.x, base_pos.y, base_pos.z);
        // pos���� �÷��̾��� x�� ��.
    }
    // base_pos�� ������Ʈ�ϴ� �޼��� �߰�
    public void UpdateBasePos(Vector3 newPos)
    {
        base_pos = newPos;
    }
}
