using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Calculation;
using Unity.VisualScripting;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    private Player player;
    private Vector2Int goal;
    private List<Cell> openList, closedList;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        AStar();
    }

    private void InitVariables()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        player = GetComponent<Player>();

        openList = new List<Cell>();
        closedList = new List<Cell>();
    }

    private void AStar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetAStarGoal();
            AStarTraverse();
        }
    }

    // 우클릭했을 때 해당 좌표를 가져온다
    // 함수 원리 잘 이해 못함 ray.GetPoint? ray.origin.y?
    private void SetAStarGoal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
        goal = Calc.Vector3to2Int(grid.WorldToCell(worldPoint));

        //print("Vefore : " + grid.WorldToCell(worldPoint));
        print("GOAL POSITION : " + goal);
    }

    private void AStarTraverse()
    {
        // 현재 플레이어의 위치
        Vector2Int startPos = player.GetCurrentPosition();
        float startG = 0;
        float startH = Calc.ManhattenDistance(startPos, goal);

        AStarGrid aStarGrid = new AStarGrid();

        aStarGrid.aStarGrid.Add(startPos, new Cell(startG, startH));

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (startPos.x == i && startPos.y == j)
                    continue;

                Vector2Int candidate = new Vector2Int(startPos.x + i, startPos.y + j);

                float g = Calc.ManhattenDistance(startPos, candidate);
                float h = Calc.ManhattenDistance(candidate, goal);


                aStarGrid.aStarGrid.Add(candidate, new Cell(g, h));
            }
        }



        foreach (KeyValuePair<Vector2Int, Cell> keyValuePair in aStarGrid.aStarGrid)
        {
            print(keyValuePair.Key);
            print(keyValuePair.Value.PrintCell());
        }



        //print(aStarGrid.aStarGrid.Count);

        //while (true)
        //{



        //}
    }
}
