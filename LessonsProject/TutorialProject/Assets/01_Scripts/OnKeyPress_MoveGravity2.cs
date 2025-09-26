using UnityEngine;

// 점프
public class OnKeyPress_TestMoveGravity2 : MonoBehaviour
{
    public float speed = 3;
    public float jumppower = 8; // 점프력

    float vx = 0;
    bool leftFlag = false; // 왼쪽 방향인지

    bool groundFlag = false; // 발에 뭐가 닿았는지

    Rigidbody2D rbody;

    public GameObject newPrefab;
    GameObject newGameObject;

    public float throwX = 4f; // 던지는 힘
    public float throwY = 8f; // 던지는 힘
    public float offsetY = 1f; // 캐릭터 윗부분에서 프리팹

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
    private void OnBecameInvisible() // 화면 밖으로 벗어나면 삭제된다!
    {
        Destroy(this.gameObject);
    }
}
