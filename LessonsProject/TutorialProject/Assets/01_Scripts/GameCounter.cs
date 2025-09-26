using UnityEngine;

public class GameCounter : MonoBehaviour
{
    public static int value;

    public int startCount = 0;

    void Start()
    {
        value = startCount; // 카운터를 리셋
    }
}
