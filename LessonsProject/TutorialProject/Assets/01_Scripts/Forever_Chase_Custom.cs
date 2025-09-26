using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forever_Chase_Custom : MonoBehaviour
{

    public string targetObjectName;
    public float speed = 1f;

    GameObject targetObject;
    Rigidbody2D rbody;

    public bool isChasing = true;

    void Start()
    { 
        targetObject = GameObject.Find(targetObjectName);

        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    void FixedUpdate()
    {
        if (!isChasing) return;

        Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;
        float vx = dir.x * speed;
        float vy = dir.y * speed;

        rbody.linearVelocity = new Vector2(vx, vy);//dir*speed; //new Vector2(dir.x * speed,dir.y * speed);
        this.GetComponent<SpriteRenderer>().flipX = (vx < 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isChasing = false;
        rbody.linearVelocity = Vector2.zero;

        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            GameMan.instance.Spawner();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Invoke("SetChasingTrue", 1f);
    }

    private void SetChasingTrue()
    {
        isChasing = true;
    }
}