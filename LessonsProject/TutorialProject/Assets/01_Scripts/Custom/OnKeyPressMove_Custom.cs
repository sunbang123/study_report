using Unity.VisualScripting;
using UnityEngine;

public class OnKeyPressMove_Custom : MonoBehaviour
{
    public float speed = 2;
    public float rotationSpeed = 10f; // ȸ�� �ӵ�
    public static OnKeyPressMove_Custom instance;
    float vx = 0;
    float vy = 0;
    bool leftFlag = false;
    Rigidbody2D rbody;
    public GameObject Bait;

    void Start()
    {
        instance = this.GetComponent<OnKeyPressMove_Custom>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        vx = 0;
        vy = 0;
        if (Input.GetKey("right"))
        {
            vx = speed;
            leftFlag = false;
        }
        if (Input.GetKey("left"))
        {
            vx = -speed;
            leftFlag = true;
        }
        if (Input.GetKey("up"))
        {
            vy = speed;
        }
        if (Input.GetKey("down"))
        {
            vy = -speed;
        }
    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(vx, vy);

        // ���������� �ڿ������� ȸ��
        Quaternion targetRotation = Quaternion.Euler(0, leftFlag ? 0 : 180, 0);
        this.transform.rotation = Quaternion.Slerp(
            this.transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        //�Լ� FixedUpdate():
        //    rbody�� �ӵ� �� (vx, vy)

        //    // �ڿ������� ȸ��
        //    ����(leftFlag�� true):
        //        ��ǥȸ�� �� (0, 0, 0)
        //    �ƴϸ�:
        //        ��ǥȸ�� �� (0, 180, 0)
        //    ��
        //        ����ȸ���� ��ǥȸ������ �ε巴�� ����
        //    (Slerp ���, �ӵ� = rotationSpeed * deltaTime)
        //��
    }

    public void StepBack()
    {
        Debug.Log("Trigger entered by: " + this.gameObject.name);

        if (leftFlag)
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        }

        SpriteRenderer spriteRenderer = Bait.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", 0.2f);
    }

    void ResetColor()
    {
        SpriteRenderer spriteRenderer = Bait.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }
}