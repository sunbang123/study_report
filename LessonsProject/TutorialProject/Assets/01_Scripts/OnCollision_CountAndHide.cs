using UnityEngine;

public class OnCollision_CountAndHide : MonoBehaviour
{
    public string targetObjectName;
    public int addValue = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == targetObjectName)
        {
            GameCounter.value += addValue;

            this.gameObject.SetActive(false);
        }
    }
}
