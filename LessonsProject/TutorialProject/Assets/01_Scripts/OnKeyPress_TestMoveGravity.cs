using UnityEngine;

// 점프
public class OnKeyPress_TestMoveGravity : MonoBehaviour
{
    public float speed = 3;
    public float jumppower = 8; // 점프력

    float vx = 0;
    bool leftFlag = false; // 왼쪽 방향인지
    bool groundFlag = false; // 발에 뭐가 닿았는지
    public int count = 0;
    public int maxCount = 3;

    Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if(Input.GetKey("right"))
        {
            vx = speed;
            leftFlag = false;
        }
        if(Input.GetKey("left"))
        {
            vx = -speed;
            leftFlag = true;
        }

        if (Input.GetButtonDown("Jump") && groundFlag && count < 2)
        {
            groundFlag = false;
            rbody.AddForce(new Vector2(0, jumppower), ForceMode2D.Impulse);
            count++;
        }

        rbody.linearVelocity = new Vector2(vx, rbody.linearVelocity.y);
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
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
