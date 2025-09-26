using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ű�� ������ ��������Ʈ�� �̵��Ѵ�(��������) 
public class OnKeyPress_MoveSpriteH : MonoBehaviour
{

    public float speed = 0.7f; // �ӵ� : Inspector�� ����

    private float vx = 0f;
    private float vy = 0f;

    void Update()
    { // ��� �����Ѵ�
        vx = 0f;
        vy = 0f;
        if (Input.GetKey("right"))// ���� ������ Ű�� ������
        {
            vx = speed; // ���������� �����ϴ� �̵����� �ִ´� 
        }
        if (Input.GetKey("left"))// ���� ���� Ű�� ������
        {
            vx = -speed; // �������� �����ϴ� �̵����� �ִ´� 
        }
    }
    void FixedUpdate()// ��� �����Ѵ�(���� �ð�����) 
    {
        this.transform.Translate(vx / 50f, vy / 50f, 0);
    }


}