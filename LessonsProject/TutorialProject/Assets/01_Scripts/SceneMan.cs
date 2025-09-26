using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public GameObject bg;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = bg.GetComponent<SpriteRenderer>();
    }

    public void GoToLobby()
    {
        InvokeRepeating("BgFade", 0f, 0.05f);
        Invoke("LoadLobby", 1.5f);
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void BgFade()
    {
        // 현재 색상을 HSV로 변환
        Color currentColor = spriteRenderer.color;
        Color.RGBToHSV(currentColor, out float h, out float s, out float v);
        v *= (float)0.97; // 밝기를 3%씩 줄임
        spriteRenderer.color = Color.HSVToRGB(h, s, v);
    }

}
