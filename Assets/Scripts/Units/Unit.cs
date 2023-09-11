using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isSelected { get; set; }

    protected Vector3Int currentPos { get; set; }

    protected float maxHp, currentHp;
    public int moveRange { get; protected set; }
    
    protected virtual void Start()
    {
        InitVariables();
    }

    private void InitVariables()
    {
        isSelected = false;
    }

    public Vector2Int GetCurrentPosition()
    {
        currentPos = GlobalGrid.Instance.Grid.WorldToCell(transform.position);

        return Calc.Vector3to2Int(currentPos);
    }

    public Vector3Int GetCurrentPositionVector3Int()
    {
        currentPos = GlobalGrid.Instance.Grid.WorldToCell(transform.position);

        return currentPos;
    }

    public virtual void DrawMoveRange()
    {

    }
}