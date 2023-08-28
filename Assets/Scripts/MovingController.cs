using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingController : MonoBehaviour
{
    public Tilemap map;

    private Grid grid;


    private void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        //PlayerInput();

        Click();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * 0.5f;
        }
    }

    private void MoveMove()
    {
        transform.position += new Vector3(0.5f, 0, 0);

    }

    private void Click()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int pos = grid.WorldToCell(mouseWorldPos);

            print(mouseWorldPos);
            print(pos);
        }
    }
}
