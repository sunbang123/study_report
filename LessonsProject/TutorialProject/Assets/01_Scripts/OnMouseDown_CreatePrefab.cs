using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDown_CreatePrefab : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject newPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭 할때마다

            // 클릭한 위치를 카메라 안에서 위치로 변환

            // 앞쪽에 표시
        if(Input.GetMouseButtonDown(0))
        {
            // 카메라앞에서 나오도록 하기
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);

            GameObject newGameObject = Instantiate(newPrefab);

            newGameObject.transform.position = pos;
        }
    }
}
