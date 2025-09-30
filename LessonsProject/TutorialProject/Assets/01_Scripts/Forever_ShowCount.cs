using UnityEngine;
using UnityEngine.UI;

public class Forever_ShowCount : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = GameCounter.value.ToString();
    }
}
