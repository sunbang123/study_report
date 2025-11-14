// 반복되는 배경

using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Transform Target;
    public float ScrollSpeed = 0.1f;

    private Material _backgroundMaterial;

    private void Start()
    {
        _backgroundMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(Target == null)
            return;

        transform.position = Target.position;

        float offsetX = (Target.position.x * ScrollSpeed) % 1.0f;
        float offsetY = (Target.position.y * ScrollSpeed) % 1.0f;

        _backgroundMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
