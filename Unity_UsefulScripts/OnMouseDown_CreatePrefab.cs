using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDown_CreatePrefab : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);

            GameObject newGameObject = Instantiate(newPrefab);

            newGameObject.transform.position = pos;
        }
    }
}
