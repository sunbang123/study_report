using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ġ�� �귿ó�� ȸ����.
public class OnMouseDown_Roulette : MonoBehaviour
{
    public float maxSpeed = 50; // �ִ�ӵ� inspector�� ������~

    float rotateAngle = 0;
    private bool rewardFlag = false; // ����޾Ҵ��� üũ

    // ȸ������ 0.98�� ���ؼ� �ٿ�����
    private void OnMouseDown()
    {
        // ��ġ�ϸ� �ִ�ӵ��� ����!
        rotateAngle = maxSpeed;
        InvokeRepeating("OnReward", 2f, 0.5f); // 2���ĺ��� 0.5�ʸ��� ����üũ
    }
    private void OnReward()
    {
        if (rotateAngle == 0 && !rewardFlag)
        {
            Debug.Log("Reward!");
            rewardFlag = true; // ���� Flag -> true
            rewardFlag = GameMan.Reward(this.transform); // ���߸� ����
            CancelInvoke("OnReward"); // �ݺ����
        }
    }
    private void FixedUpdate()
    {
        // ��� ���� (�����ð�����) why? 360�� ���Ϸ��� 1000�� ȸ���� 280�� ȸ���� ������ ��� �����پ��µ�, �ٽû������⵵ �ؿ�
        //rotateAngle--;
        rotateAngle = rotateAngle * (float)0.98; // ȸ������ ���ݾ�
        this.transform.Rotate(0, 0, rotateAngle); // ȸ���Ѵ�.
        if (rotateAngle < 0.1f && rotateAngle > 0f) rotateAngle = 0f;
    }
}
