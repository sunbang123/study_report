using UnityEngine;

public class GameMan : MonoBehaviour
{
    public static GameMan instance;
    public GameObject Ghost;
    public GameObject Shadow;
    public GameObject TreasurePrefab;
    public Sprite Key;
    public Sprite Cherry;
    public Sprite Peach;
    public static GameMan Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        instance.Ghost.GetComponent<Collider2D>().isTrigger = false;
    }

    public static bool Reward(Transform roulette)
    {
        string result = null;
        float z = roulette.rotation.eulerAngles.z;

        if (z < 45f) result = "Pink";
        else if (z >= 45f && z < 135f) result = "Red";
        else if (z >= 135f && z < 225f) result = "Yellow";
        else result = "Cyan";

        Debug.Log(result);

        switch (result)
        {
            case "Pink":
                instance.Ghost.GetComponent<SpriteRenderer>().sprite = instance.Peach; 
                instance.Shadow.GetComponent<SpriteRenderer>().sprite = instance.Peach;
                break;
            case "Cyan":
                instance.Shadow.SetActive(false);
                instance.Ghost.GetComponent<Forever_Chase_Custom>().isChasing = true;
                break;
            case "Yellow":
                instance.Shadow.SetActive(false);
                instance.Ghost.GetComponent<SpriteRenderer>().sprite = instance.Key;
                instance.Ghost.GetComponent<Collider2D>().isTrigger = true;
                break;
            case "Red":
                instance.Ghost.GetComponent<SpriteRenderer>().sprite = instance.Cherry;
                instance.Shadow.GetComponent<SpriteRenderer>().sprite = instance.Cherry;
                break;
        }

        return false;
    }

    public void Spawner()
    {
        Instantiate(TreasurePrefab);
    }
}
