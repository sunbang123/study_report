using UnityEngine;

public class csTankMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public GameObject turret;

    void Update()
    {
        float v = Input.GetAxis("Vertical"); // 1 ~ -1
        float h = Input.GetAxis("Horizontal"); // 1 ~ -1

        v = v * moveSpeed * Time.deltaTime;
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
