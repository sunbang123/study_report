using UnityEngine;

public class MoveTank : MonoBehaviour
{

    public float speed = 2;

    float vx = 0;
    float vy = 0;
    bool leftFlag = false;
    Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        //rbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
    void Update()
    {
        vx = 0;
        vy = 0;
        if (Input.GetKey("up"))
        {
            vx = speed;
            leftFlag = false;
        }
        if (Input.GetKey("down"))
        {
            vx = -speed;
            leftFlag = true;
        }
        if(Input.GetKey("left"))
        {
            rbody.AddRelativeTorque(0, 10f, 0);
        }
        if (Input.GetKey("right"))
        {
            rbody.AddRelativeTorque(0, -10f, 0);
        }
    }
    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector3(vx, rbody.linearVelocity.y, vy);
    }
}
