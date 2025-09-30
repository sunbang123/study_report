using UnityEngine;

public class csTankMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public GameObject turret;

    void Update()
    {
        // GetAxis는 실수가 나옴!
        // 이동 키보드 입력
        float v = Input.GetAxis("Vertical"); // 1 ~ -1값이 나옴.
        // 회전 키보드 입력
        float h = Input.GetAxis("Horizontal"); // 1 ~ -1값이 나옴.

        // 이동거리보정
        // 방향 * 크기 * 보정치
        v = v * moveSpeed * Time.deltaTime;
        // 회전거리 보정
        h = h * rotateSpeed * Time.deltaTime;

        if (v == 0)
            turret.transform.Rotate(Vector3.up * h);
        else
        {
            this.transform.Translate(Vector3.forward * v);
            transform.Rotate(Vector3.up * h);
        }
    }
}
