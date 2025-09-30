using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 0.1f;

    public void TranslateDown()
    {
        Vector3 position = this.gameObject.transform.position;
        position.y -= 1f * speed;
        this.gameObject.transform.position = position;
    }
    public void TranslateUp()
    {
        Vector3 position = this.gameObject.transform.position;
        position.y += 1f * speed;
        this.gameObject.transform.position = position;
    }
}
