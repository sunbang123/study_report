using System;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public string targetObjectName;
    public static event Action OnPotionCollected;
    private Rigidbody2D rbody;
    public float fallSpeed = 0.5f; // 낙하 속도
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
            // 이벤트 발생~! 구독자들에게 알림
            OnPotionCollected?.Invoke();
        }
    }

    void FixedUpdate()
    {
        // 물약은 서서히 바닥으로 이동 (Rigidbody2D 사용)
        if (rbody != null)
        {
            rbody.linearVelocity = new Vector2(0, -fallSpeed);
        }

        // 물약이 바닥에 떨어지면 사라지게 하기
        if (this.transform.position.y < -5f)
        {
            this.gameObject.SetActive(false);
        }
    }
    // 이벤트
    //    StoneFish의 Forever_Avoid 컴포넌트 비활성화
    //    StoneFish의 Forever_Chase 컴포넌트 활성화
    // 이벤트는 2초간 진행 
    // 이벤트는 UnderWater_GameMan에서 진행
}
