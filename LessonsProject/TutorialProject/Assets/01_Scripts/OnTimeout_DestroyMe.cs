using UnityEngine;

public class OnTimeout_DestroyMe : MonoBehaviour
{
    public float limitSec = 3;
    private void Start()
    {
        Destroy(this.gameObject, limitSec);
    }
}
