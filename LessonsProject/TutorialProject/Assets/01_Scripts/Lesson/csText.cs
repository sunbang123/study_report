using UnityEngine;
using UnityEngine.UI;

public class csText : MonoBehaviour
{
    public string words = "GoodBye World";
    GameObject obj;
    Text txt;
    string temp;
    bool textFlag = false;
    private void Start()
    {
        obj = GameObject.Find("txtCenter");
        txt = obj.GetComponent<Text>();
        temp = txt.text;
    }

    public void ChangeText()
    {
        if (!textFlag)
        {
            txt.text = words;
            textFlag = true;
        }
        else
        {
            txt.text = temp;
            textFlag = false;
        }
        Debug.Log(txt.text);
    }
}