using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    private RaycastHit hit;

    private void Start()
    {

    }

    private void Update()
    {
        MouseClickToSelectUnit();
    }

    private void MouseClickToSelectUnit()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Unit")))
            {
                print(hit.collider.gameObject.name);
            }
        }
    }
}
