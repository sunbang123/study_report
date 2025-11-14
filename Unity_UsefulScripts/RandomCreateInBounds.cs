using UnityEngine;

public class RandomCreateInBounds : MonoBehaviour
{
    public GameObject newPrefab;
    public float intervalSec = 1f;
    public Vector3 newPos;

    private void Start()
    {
        InvokeRepeating("CreatePrefab", intervalSec, intervalSec);
    }

    private void CreatePrefab()
    {
        Vector3 area = this.GetComponent<SpriteRenderer>().bounds.size;
        newPos = this.transform.position;

        newPos.x += Random.Range(-area.x / 2, area.x / 2);
        newPos.y += Random.Range(-area.y / 2, area.y / 2);
        newPos.z = 1f;

        GameObject newGameObject = Instantiate(newPrefab, newPos, Quaternion.identity);
    }
}
