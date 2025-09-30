using UnityEngine;

public class OnMouseDown_Custom : MonoBehaviour
{
    public float maxSpeed = 50;

    float rotateAngle = 0;
    private bool rewardFlag = false;
    private void OnMouseDown()
    {
        rotateAngle = maxSpeed;
        InvokeRepeating("OnReward", 2f, 0.5f);
    }
    private void OnReward()
    {
        if (rotateAngle == 0 && !rewardFlag)
        {
            Debug.Log("Reward!");
            rewardFlag = true; // ���� Flag -> true
            rewardFlag = GameMan.Reward(this.transform);
            CancelInvoke("OnReward");
        }
    }
    private void FixedUpdate()
    {
        rotateAngle = rotateAngle * (float)0.98;
        this.transform.Rotate(0, 0, rotateAngle);
        if (rotateAngle < 0.1f && rotateAngle > 0f) rotateAngle = 0f;
    }
}
