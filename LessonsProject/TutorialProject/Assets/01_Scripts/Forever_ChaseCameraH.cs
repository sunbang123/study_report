using UnityEngine;
public class Forever_ChaseCameraH : MonoBehaviour
{
    Vector3 base_pos;

    private void Start()
    {
        base_pos = Camera.main.gameObject.transform.position;
    }

    private void LateUpdate() // 플레이어가 움직이고 카메라가 움직여야되니까
    {
        Vector3 pos = this.transform.position;
        Camera.main.gameObject.transform.position = new Vector3(pos.x, base_pos.y, base_pos.z);
        // pos값만 플레이어의 x를 씀.
    }
}
