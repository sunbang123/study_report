using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    Rigidbody rb;
    Renderer ballColor;
    public float jumpPower;
    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        ballColor = this.GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 3)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            ballColor.material.color = new Color(Random.Range(0, 255) * 0.01f, Random.Range(0, 255) * 0.01f, Random.Range(0, 255) * 0.01f, 255);
            Debug.Log(ballColor.material.color);
        }
    }
}
