using UnityEngine;

// ����
public class OnKeyPress_TestMoveGravity2 : MonoBehaviour
{
    public float speed = 3;
    public float jumppower = 8; // ������

    float vx = 0;
    bool leftFlag = false; // ���� ��������

    bool groundFlag = false; // �߿� ���� ��Ҵ���

    Rigidbody2D rbody;

    public GameObject newPrefab;
    GameObject newGameObject;

    public float throwX = 4f; // ������ ��
    public float throwY = 8f; // ������ ��
    public float offsetY = 1f; // ĳ���� ���κп��� ������

    Vector3 newPos;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
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

        if (Input.GetButtonDown("Jump") && groundFlag)
        {
            groundFlag = false;
            rbody.AddForce(new Vector2(0, jumppower), ForceMode2D.Impulse);
        }

        rbody.linearVelocity = new Vector2(vx, rbody.linearVelocity.y);
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;

        if(Input.GetMouseButtonDown(0))
        {
            newPos = this.transform.position;

            newPos.y += offsetY;

            newGameObject = Instantiate(newPrefab);

            newGameObject.transform.position = newPos;

            Rigidbody2D rbody2 = newGameObject.GetComponent<Rigidbody2D>();

            if(leftFlag)
            {
                rbody2.AddForce(new Vector2(-throwX, throwY), ForceMode2D.Impulse);
            }
            else
            {
                rbody2.AddForce(new Vector2(throwX, throwY), ForceMode2D.Impulse);
            }
        }

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        groundFlag = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        groundFlag = false;
    }
    private void OnBecameInvisible() // ȭ�� ������ ����� �����ȴ�!
    {
        Destroy(this.gameObject);
    }
}
