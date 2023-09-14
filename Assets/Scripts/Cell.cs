using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int pos;
    public float f { get; private set; }
    public float g { get; private set; }
    public float h { get; private set; }
    public Cell prevCell { get; set; }
    public Vector3 dir { get; set; }

    //public Cell(float g, float h)
    //{
    //    this.g = g;
    //    this.h = h;

    //    this.f = g + h;
    //}

    public Cell(Vector2Int pos, float g, float h)
    {
        this.pos = pos;
        this.g = g;
        this.h = h;

        this.f = g + h;
    }

    public void UpdateValues(float g, float h)
    {
        this.g = g;
        this.h = h;

        this.f = g + h;
    }

    public string PrintCell()
    {
        //print("Cell Position : " + pos);
        //return "f : " + f + ", g : " + g + ", h : " + h;
        return "(" + pos.x + ", " + pos.y + ") / f : " + f + ", g : " + g + ", h : " + h;
    }
}