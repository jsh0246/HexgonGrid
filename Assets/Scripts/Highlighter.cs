using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private Grid grid;

    private Tilemap tilemap;
    private RaycastHit hit;
    private Color originalColor;

    private GameObject o;
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
        tilemap = grid.GetComponentInChildren<Tilemap>();
        highlighted = false;
    }

    private void HightLightGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Grid")))
        {
            if (!highlighted)
            {
                o = hit.collider.gameObject;
                originalColor = hit.collider.GetComponent<MeshRenderer>().material.color;
                print("Grass Color = " + originalColor);
                hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                highlighted = true;
            }
        }

        if(highlighted && hit.collider == null)
        {
            print("MMMEEE");
            print(originalColor);
            o.GetComponent<MeshRenderer>().material.color = originalColor;
            highlighted = false;
        }
    }

    //private void Highlight()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    print(ray.origin);
    //    print(ray.direction);

    //    Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
    //    Vector3Int selectedGridPosition = grid.WorldToCell(worldPoint);

    //    print(selectedGridPosition);
    //    //selectedGridPosition = new Vector3Int(selectedGridPosition.x, 0, selectedGridPosition.y);

    //    //print(tileMap.Equals(pTilemap));


    //    print("POSITION : " + selectedGridPosition);
    //    tileMap.SetTile(selectedGridPosition, null);

    //    pTilemap.SetTile(selectedGridPosition, null);







    //    //TileBase t = tileMap.GetTile(selectedGridPosition);
    //}
}
