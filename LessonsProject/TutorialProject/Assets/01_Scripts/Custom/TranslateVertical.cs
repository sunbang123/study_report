using UnityEngine;

public class TranslateVertical : MonoBehaviour
{
    public float speed = 0.1f;

    public void TranslateDown()
    {
        // 서서히 내려가도록
        Vector3 position = this.gameObject.transform.position;
        position.y -= 1f * speed;
        this.gameObject.transform.position = position;
    }

    public void TranslateUp()
    {
        // 서서히 올라가도록
        Vector3 position = this.gameObject.transform.position;
        position.y += 1f * speed;
        this.gameObject.transform.position = position;
    }

    // 필요시 이동 멈추기
    public void StopMoving()
    {
        CancelInvoke();
    }

    // 위로 이동으로 전환
    public void StartMovingUp()
    {
        StopMoving();
        InvokeRepeating("TranslateUp", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }

    // 아래로 이동으로 전환
    public void StartMovingDown()
    {
        StopMoving();
        InvokeRepeating("TranslateDown", 0f, 0.05f);
        Invoke("StopMoving", 0.5f);
    }
}
