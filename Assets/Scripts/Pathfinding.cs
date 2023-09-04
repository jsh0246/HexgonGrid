using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int f, g, h;

    public Cell(int g, int h)
    {
        this.f = g + h;
        this.g = g;
        this.h = h;
    }
}

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Grid grid;

    private List<Cell> openList, closedList;

    private void Start()
    {
        openList = new List<Cell>();
        closedList = new List<Cell>();
    }

    private void Update()
    {
        MousePositionToGridCoordinate();
    }

    // 클랙했을 때 해당 좌표를 가져온다
    private Vector3Int MousePositionToGridCoordinate()
    {
        Vector3Int pos = Vector3Int.zero;

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = grid.WorldToCell(mouseWorldPos);

            print(pos);
        }

        return pos;
    }

    // A* Algorithm
    private void AStar()
    {

    }


}
