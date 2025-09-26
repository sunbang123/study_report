using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 터치시 룰렛처럼 회전함.
public class OnMouseDown_Roulette : MonoBehaviour
{
    public float maxSpeed = 50; // 최대속도 inspector에 저장함~

    float rotateAngle = 0;
    private bool rewardFlag = false; // 보상받았는지 체크

    // 회전량에 0.98씩 곱해서 줄여주자
    private void OnMouseDown()
    {
        // 터치하면 최대속도를 낸다!
        rotateAngle = maxSpeed;
        InvokeRepeating("OnReward", 2f, 0.5f); // 2초후부터 0.5초마다 보상체크
    }
    private void OnReward()
    {
        if (rotateAngle == 0 && !rewardFlag)
        {
            Debug.Log("Reward!");
            rewardFlag = true; // 보상 Flag -> true
            rewardFlag = GameMan.Reward(this.transform); // 멈추면 보상
            CancelInvoke("OnReward"); // 반복취소
        }
    }
    private void FixedUpdate()
    {
        // 계속 시행 (일정시간마다) why? 360도 오일러각 1000도 회전은 280도 회전과 동일한 결과 점점줄어드는데, 다시빨라지기도 해용
        //rotateAngle--;
        rotateAngle = rotateAngle * (float)0.98; // 회전량을 조금씩
        this.transform.Rotate(0, 0, rotateAngle); // 회전한다.
        if (rotateAngle < 0.1f && rotateAngle > 0f) rotateAngle = 0f;
    }
}
