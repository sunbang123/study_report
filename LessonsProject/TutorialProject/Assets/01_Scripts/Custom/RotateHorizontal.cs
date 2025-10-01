using UnityEngine;

public class RotateHorizontal : MonoBehaviour
{
    public float speed = 0.1f;

    public void RotateRight()
    {
        // Z축 기준 시계방향 회전
        this.transform.Rotate(0, 0, -speed);
    }

    public void RotateLeft()
    {
        // Z축 기준 반시계방향 회전
        this.transform.Rotate(0, 0, speed);
    }

    // 필요시 이동 멈추기
    public void StopMoving()
    {
        CancelInvoke();
    }

    // 위로 이동으로 전환
    public void StartRotateRight()
    {
        StopMoving();
        InvokeRepeating("RotateRight", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }

    // 아래로 이동으로 전환
    public void StartRotateLeft()
    {
        StopMoving();
        InvokeRepeating("RotateLeft", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }
}
