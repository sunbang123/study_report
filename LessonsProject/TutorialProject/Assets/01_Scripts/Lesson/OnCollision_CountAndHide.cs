using UnityEngine;

public class OnCollision_CountAndHide : MonoBehaviour
{
    public string targetObjectName;
    public int addValue = 1;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name == targetObjectName)
        {
            GameCounter.value += addValue;

            this.gameObject.SetActive(false);
        }
    }
}
