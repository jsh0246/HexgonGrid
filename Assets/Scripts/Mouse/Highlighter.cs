using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Highlighter : MonoBehaviour
{
    private UnitSelector unitSelector;

    private RaycastHit hit;
    private Color originalColor;
    private GameObject gridObject;
    private bool highlighted;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        HightLightGameObject();
    }

    private void InitVariables()
    {
        unitSelector = GetComponent<UnitSelector>();

        highlighted = false;
    }

    private void HightLightGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Grid")))
        {
            if (!highlighted)
            {
                gridObject = hit.collider.gameObject;
                originalColor = hit.collider.GetComponent<MeshRenderer>().material.color;
                hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                highlighted = true;
            }
        }

        if(highlighted && hit.collider == null)
        {
            gridObject.GetComponent<MeshRenderer>().material.color = originalColor;
            highlighted = false;
        }
    }
}
