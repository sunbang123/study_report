// 2D Move

public class OnKeyPress_Move : MonoBehaviour
{

    public float speed = 2;

    float vx = 0;
    float vy = 0;
    bool leftFlag = false;
    Rigidbody2D rbody;

    void Start()
    { 
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
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
    }
}

// 응용 - Jump까지 할수있는 2D Move

using UnityEngine;

public class OnKeyPress_TestMoveGravity2 : MonoBehaviour
{
    public float speed = 3;
    public float jumppower = 8;

    float vx = 0;
    bool leftFlag = false;

    bool groundFlag = false;

    Rigidbody2D rbody;

    public GameObject newPrefab;
    GameObject newGameObject;

    public float throwX = 4f;
    public float throwY = 8f;
    public float offsetY = 1f;

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
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

// 응용 - 따라가기

public class Forever_Chase : MonoBehaviour
{

    public string targetObjectName;
    public float speed = 1f;

    GameObject targetObject;
    Rigidbody2D rbody;

    void Start()
    { 
        targetObject = GameObject.Find(targetObjectName);

        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;
        float vx = dir.x * speed;
        float vy = dir.y * speed;

        rbody.linearVelocity = new Vector2(vx, vy);//dir*speed; //new Vector2(dir.x * speed,dir.y * speed);
        this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
    }
}
