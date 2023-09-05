using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Calculation;

public class Player : MonoBehaviour
{
    private Grid grid;
    public Vector3Int currentPos { get; private set; }

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        GetCurrentPosition();
    }

    private void InitVariables()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    public Vector2Int GetCurrentPosition()
    {
        currentPos = grid.WorldToCell(transform.position);

        //print(Calc.Vector3to2Int(currentPos));

        return Calc.Vector3to2Int(currentPos);
    }
}