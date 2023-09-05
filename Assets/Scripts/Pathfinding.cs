using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Calculation;
using System.Linq;
using System;

public class ByCellFValue : IComparer<Vector2Int>
{

    //public bool Compare(Vector2Int x, Vector2Int y)
    //{
    //    return AStarGrid.aStarGrid[x].f < AStarGrid.aStarGrid[y].f;
    //}

    //int IComparer<Vector2Int>.Compare(Vector2Int x, Vector2Int y)
    //{
    //    if (AStarGrid.aStarGrid[x].f < AStarGrid.aStarGrid[y].f) return -1;
    //    else if (AStarGrid.aStarGrid[x].f > AStarGrid.aStarGrid[y].f) return 1;
    //    else return 0;
    //}

    public int Compare(Vector2Int x, Vector2Int y)
    {
        if (AStarGrid.aStarGrid[x].f == AStarGrid.aStarGrid[y].f)
        {
            if (x == y) return 0;
        }

        if (AStarGrid.aStarGrid[x].f < AStarGrid.aStarGrid[y].f)
            return -1;
        else if (AStarGrid.aStarGrid[x].f > AStarGrid.aStarGrid[y].f)
            return 1;
        else
            return 0;





        //else if(AStarGrid.aStarGrid[x].f > AStarGrid.aStarGrid[y].f) return 1;

        //if (AStarGrid.aStarGrid[x].f < AStarGrid.aStarGrid[y].f) return -1;
        //else if (AStarGrid.aStarGrid[x].f > AStarGrid.aStarGrid[y].f) return 1;
        //else return 0;

        //if (AStarGrid.aStarGrid[x].f <= AStarGrid.aStarGrid[y].f) return -1;
        //else return 1;
    }
}

public class FValueComparer : IComparer<Vector2Int>
{
    public int Compare(Vector2Int x, Vector2Int y)
    {
        //if(AStarGrid.aStarGrid[x].f == AStarGrid.aStarGrid[y].f)
        //{
        //    if (x.x == y.x && x.y == y.y) return 0;
        //}

        if (x.x == y.x && x.y == y.y) return 0;
        //if (x == y) return 0;

        if (AStarGrid.aStarGrid[x].f < AStarGrid.aStarGrid[y].f)
            return -1;
        else if (AStarGrid.aStarGrid[x].f >= AStarGrid.aStarGrid[y].f)
            return 1;
        else return 1;
    }
}

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    private Player player;
    private Vector2Int start, goal;
    //private SortedSet<Vector2Int> openList;
    private SortedDictionary<Vector2Int, float> openList;
    private HashSet<Vector2Int> closedList;


    //private SortedList<Vector2Int, int> 

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

        openList = new SortedDictionary<Vector2Int, float>(new FValueComparer());
        //openList = new SortedSet<Vector2Int>();
        closedList = new HashSet<Vector2Int>();
        //aStarGrid = new Dictionary<Vector2Int, Cell>();
    }

    private void AStar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetAStarGoal();

            start = player.GetCurrentPosition();
            float g = 0;
            float h = Calc.ManhattenDistance(start, goal);

            AStarGrid.aStarGrid.Add(start, new Cell(g, h));
            openList.Add(start, g+h);

            AStarTraverse(start);
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
        //print("GOAL POSITION : " + goal);
    }

    private void AStarTraverse(Vector2Int pos)
    {
        // 현재 플레이어의 위치
        //Vector2Int startPos = player.GetCurrentPosition();


        // OpenList에 시작점 추가
        // 하려고 했는데 할필요가 있나 싶네?
        //openList.Add(startPos, new Cell(startG, startH));

        // 시작점의 8방향 탐색 후 OpenList에 추가
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                Vector2Int adjacentPos = new Vector2Int(pos.x + i, pos.y + j);

                float _g = Calc.ManhattenDistance(start, adjacentPos);
                float _h = Calc.ManhattenDistance(adjacentPos, goal);

                try
                {
                    AStarGrid.aStarGrid.Add(adjacentPos, new Cell(_g, _h));
                    openList.Add(adjacentPos, _g+_h);
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }

        foreach (var v in openList)
            print(v);

        // 시작점은 OpenList에서 제거 후 ClosedList로
        print(openList.Count);
        print(openList.Remove(pos));
        print(openList.Count);


        closedList.Add(pos);

        // OpenList의 최소값 하나 선택
        //print(openList.ElementAt(0));

        
        foreach (var v in openList)
            print(v);


        //var sortedDict = openList.OrderBy(x => x.Value.f).ToList();
        //var next = sortedDict[0];

        // 해당 좌표가 Goal이면 종료, 아니면 계속 진행
        //if(next.Key == goal)
        //{
        //    print("Arrived");
        //    return;
        //} else
        //{
        //    // 현재점을 시작으로 Loop
        //    AStarTraverse(sortedDict[0].Key);
        //}

        




        //foreach (KeyValuePair<Vector2Int, Cell> keyValuePair in aStarGrid.aStarGrid)
        //{
        //    print(keyValuePair.Key);
        //    print(keyValuePair.Value.PrintCell());
        //}


        //var items = from pair in aStarGrid.aStarGrid orderby pair.Value.f ascending select pair;
        //var sortedDict = items.ToDictionary(x => x.Key, x => x.Value);

        //var sortedDict = aStarGrid.aStarGrid.OrderBy(x => x.Value.f).ToDictionary(x => x.Key, x => x.Value);





        //var sortedDict = aStarGrid.aStarGrid.OrderBy(x => x.Value.f).ToList();
        //print(sortedDict[0].Key + " " + sortedDict[0].Value);



        //foreach (KeyValuePair<Vector2Int, Cell> keyValuePair in sortedDict)
        //{
        //    print(keyValuePair.Key);
        //    print(keyValuePair.Value.PrintCell());
        //}



    }
}