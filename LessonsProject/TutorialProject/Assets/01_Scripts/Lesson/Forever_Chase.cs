using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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