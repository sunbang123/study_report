using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Bait")
        {
            OnKeyPressMove_Custom.instance.StepBack();
        }
    }
}
