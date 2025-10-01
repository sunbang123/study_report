using System;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public string targetObjectName;
    public static event Action OnPotionCollected;
    private Rigidbody2D rbody;
    public float fallSpeed = 0.5f; // ���� �ӵ�
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            rbody.gravityScale = 0;
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == targetObjectName)
        {
            Debug.Log("Potion collected!");
            this.gameObject.SetActive(false);
            // �̺�Ʈ �߻�~! �����ڵ鿡�� �˸�
            OnPotionCollected?.Invoke();
        }
    }

    void FixedUpdate()
    {
        // ������ ������ �ٴ����� �̵� (Rigidbody2D ���)
        if (rbody != null)
        {
            rbody.linearVelocity = new Vector2(0, -fallSpeed);
        }

        // ������ �ٴڿ� �������� ������� �ϱ�
        if (this.transform.position.y < -5f)
        {
            this.gameObject.SetActive(false);
        }
    }
    // �̺�Ʈ
    //    StoneFish�� Forever_Avoid ������Ʈ ��Ȱ��ȭ
    //    StoneFish�� Forever_Chase ������Ʈ Ȱ��ȭ
    // �̺�Ʈ�� 2�ʰ� ���� 
    // �̺�Ʈ�� UnderWater_GameMan���� ����
}
