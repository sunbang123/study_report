using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDown_Rotate : MonoBehaviour
{
    public float angle = 360;
    float rotateAngle = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        //Debug.Log("���Դ�!");
        //this.GetComponent<SpriteRenderer>().flipX = true;
        rotateAngle = angle;
    }

    void OnMouseUp()
    {
        //Debug.Log("������!");
        //this.GetComponent<SpriteRenderer>().flipX = false;
        rotateAngle = 0;
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(0, 0, rotateAngle / 50);
    }
}
