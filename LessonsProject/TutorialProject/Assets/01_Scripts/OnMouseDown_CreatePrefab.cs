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
        // ��Ŭ�� �Ҷ�����

            // Ŭ���� ��ġ�� ī�޶� �ȿ��� ��ġ�� ��ȯ

            // ���ʿ� ǥ��
        if(Input.GetMouseButtonDown(0))
        {
            // ī�޶�տ��� �������� �ϱ�
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);

            GameObject newGameObject = Instantiate(newPrefab);

            newGameObject.transform.position = pos;
        }
    }
}
