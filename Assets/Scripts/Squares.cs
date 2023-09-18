using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Square
{
    public GameObject floor;

    public int x, y;
    public bool isMoveRange;
    public bool isAttackRange;
    public bool isUnitExist;

    public string PrineSquare()
    {
        string s = null;

        s += "FLOOR COORDINATE : " + floor.transform.position + '\n';
        s += "[" + x + ", " + y + "]";

        return s;
    }
}

public class Squares : MonoBehaviour
{
    public static Squares Instance { get; private set; }

    public Square[,] square { get; private set; }
    public int size { get; private set; }

    private RaycastHit hit, _hit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            SetSquaresSize(16);
            InitVariables();

            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetSquaresSize(int size)
    {
        this.size = size;
    }

    private void InitVariables()
    {
        square = new Square[size, size];
        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                square[i, j] = new Square();
            }
        }

        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                square[i, j].x = i - 7;
                square[i, j].y = j - 7;
                square[i, j].isMoveRange = false;
                square[i, j].isAttackRange = false;
                square[i, j].isUnitExist = false;

                GetGameObject(i, j, new Vector3Int((i - 7) * 2 + 1, 0, (j - 7) * 2 + 1));

                //print(square[i, j].PrineSquare());
            }
        }
    }

    private void GetGameObject(int i, int j, Vector3Int coordinate)
    {
        Ray ray = new Ray(Camera.main.transform.position, coordinate - Camera.main.transform.position);
        //Debug.DrawRay(Camera.main.transform.position, coordinate - Camera.main.transform.position, Color.red, 10f);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Grid")))
        {
            square[i, j].floor = hit.collider.gameObject;
        }
    }

    public Vector2Int GridToSquareCoordinate(Vector2Int v)
    {
        return new Vector2Int(v.x + 7, v.y + 7);
    }

    public Square GetSquare(int x, int y)
    {
        return square[x, y];
    }

    public Square GetSquare(Vector2Int v)
    {
        return square[v.x, v.y];
    }

    public Unit GetUnit(int x, int y)
    {
        Vector3 pos = GetSquare(x, y).floor.transform.position;

        Ray ray = new Ray(Camera.main.transform.position, pos - Camera.main.transform.position);
        //Debug.DrawRay(Camera.main.transform.position, pos - Camera.main.transform.position, Color.red, 10f);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Unit")))
        {
            return _hit.collider.gameObject.GetComponent<Unit>();
        }

        return null;
    }

    public Unit GetUnit(Vector2Int v)
    {
        Vector3 pos = GetSquare(v.x, v.y).floor.transform.position;

        //Camera verticalCamera = new Camera(

        Ray ray = new Ray(Camera.main.transform.position, pos - Camera.main.transform.position);
        //Debug.DrawRay(Camera.main.transform.position, pos - Camera.main.transform.position, Color.red, 10f);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, LayerMask.GetMask("Unit")))
        {
            return _hit.collider.gameObject.GetComponent<Unit>();
        }

        return null;
    }
}