using UnityEngine;
public class Forever_ChaseCameraH : MonoBehaviour
{
    Vector3 base_pos;

    private void Start()
    {
        base_pos = Camera.main.gameObject.transform.position;
    }

    private void LateUpdate() // �÷��̾ �����̰� ī�޶� �������ߵǴϱ�
    {
        Vector3 pos = this.transform.position;
        Camera.main.gameObject.transform.position = new Vector3(pos.x, base_pos.y, base_pos.z);
        // pos���� �÷��̾��� x�� ��.
    }
}
