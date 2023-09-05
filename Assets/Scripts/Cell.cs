using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    //public Vector2Int pos;
    public float f { get; private set; }
    private float g, h;

    public Cell(float g, float h)
    {
        this.g = g;
        this.h = h;

        this.f = g + h;
    }

    //public Cell(Vector2Int pos, float g, float h)
    //{
    //    this.pos = pos;
    //    this.g = g;
    //    this.h = h;

    //    this.f = g + h;
    //}

    public string PrintCell()
    {
        //print("Cell Position : " + pos);
        return "f : " + f + ", g : " + g + ", h : " + h;
    }
}