using Calculation;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EQ : IEqualityComparer<Cell>
{
    public bool Equals(Cell x, Cell y)
    {
        if (x.pos == y.pos) return true;
        else return false;
    }

    public int GetHashCode(Cell obj)
    {
        return obj.pos.GetHashCode();
    }
}

public class Pathfinding : MonoBehaviour
{
    public Cell goalCell;

    private Grid grid;
    private Player player;
    private Vector2Int start, goal;
    //private SortedSet<Vector2Int> openList;
    //private SortedDictionary<Vector2Int, float> openList;
    private HashSet<Cell> openList;
    private HashSet<Cell> closedList;
    //private Dictionary<Vector2Int, Cell> aStarGrid;


    //private SortedList<Vector2Int, int> 

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        LeftClickGivesMousePosition();
        AStar();
    }

    private void InitVariables()
    {
        goalCell = null;

        grid = GameObject.Find("Grid").GetComponent<Grid>();
        player = GetComponent<Player>();

        //openList = new SortedDictionary<Vector2Int, float>(new FValueComparer());
        openList = new HashSet<Cell>(new EQ());
        //openList = new SortedSet<Cell>(new ByCellFValue());
        closedList = new HashSet<Cell>(new EQ());
        //aStarGrid = new Dictionary<Vector2Int, Cell>();
        //aStarGrid = new HashSet<Cell>();
    }

    private void LeftClickGivesMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
            Vector2Int moousePoint = Calc.Vector3to2Int(grid.WorldToCell(worldPoint));

            

            //print("Vefore : " + grid.WorldToCell(worldPoint));
            print("MOUSE POSITION : " + moousePoint);
        }
    }

    private void AStar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetAStarGoal();

            start = player.GetCurrentPosition();
            float g = 0;
            float h = Calc.ManhattenDistance(start, goal);

            //AStarGrid.aStarGrid.Add(start, new Cell(g, h));
            //openList.Add(start, g+h);
            //aStarGrid.Add(new Cell(start, g, h));

            Cell c = new Cell(start, g, h);
            openList.Add(c);

            print("START POSITION : " + start);

            AStarTraverse(c);
        }
    }

    // ��Ŭ������ �� �ش� ��ǥ�� �����´�
    // �Լ� ���� �� ���� ���� ray.GetPoint? ray.origin.y?
    private void SetAStarGoal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
        goal = Calc.Vector3to2Int(grid.WorldToCell(worldPoint));

        //print("Vefore : " + grid.WorldToCell(worldPoint));
        print("GOAL POSITION : " + goal);
    }

    private void AStarTraverse(Cell c)
    {
        if(c.pos == goal)
        {
            print("gotcha");
            return;
        }

        print("�ο콺�̽�Ʈ : " + c.PrintCell());
        openList.Remove(c);
        // ���� �÷��̾��� ��ġ
        //Vector2Int startPos = player.GetCurrentPosition();


        // OpenList�� ������ �߰�
        // �Ϸ��� �ߴµ� ���ʿ䰡 �ֳ� �ͳ�?
        //openList.Add(startPos, new Cell(startG, startH));

        // �������� 8���� Ž�� �� OpenList�� �߰�
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                float gIncrement = 1f;
                if (Mathf.Abs(i) + Mathf.Abs(j) == 2) {
                    gIncrement += 0.4f;
                    //continue;
                }

                if (i == 0 && j == 0)
                    continue;

                Vector2Int adjacentPos = new Vector2Int(c.pos.x + i, c.pos.y + j);

                float _g = c.g + gIncrement; 
                //float _g = Calc.ManhattenDistance(start, adjacentPos);
                float _h = Calc.ManhattenDistance(adjacentPos, goal);

                try
                {
                    //AStarGrid.aStarGrid.Add(adjacentPos, new Cell(_g, _h));
                    Cell _c = new Cell(adjacentPos, _g, _h);

                    if (!closedList.Contains(_c))
                    {
                        if (openList.Contains(_c))
                        {
                            print("UPDATE");
                            foreach (Cell cell in openList)
                            {
                                if(_c.f < cell.f)
                                {
                                    cell.UpdateValues(_g, _h);
                                }
                            }
                        } else
                        {
                            openList.Add(_c);
                        }
                    }
                    else
                    {

                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }


        //foreach (var v in openList)
        //    print(v);

        // �������� OpenList���� ���� �� ClosedList��

        //foreach (var v in openList)
        //    print(v.PrintCell());

        
        closedList.Add(c);

        //openList.Remove(c);
        //closedList.Add(c);




        //foreach (var v in openList)
        //    print(v.PrintCell());




        // OpenList�� �ּҰ� �ϳ� ����
        //print(openList.ElementAt(0));

        openList = openList.OrderBy(x => x.f).ToHashSet();
        var next = openList.ElementAt(0);
        next.prevCell = c;
        //foreach (var v in openList)
        //    print(v.PrintCell());

        //foreach (var v in openList)
        //    print(v);
        //cnt++;
        //if (cnt == 20)
        //{
        //    //print("========================");
        //    //foreach (var v in openList)
        //    //    print(v.PrintCell());
        //    return;
        //}

        
        print(next.PrintCell());
        //print(openList.Count);
        //print("ClosedSet : " + closedList.Count);

        //var sortedDict = openList.OrderBy(x => x.Value.f).ToList();
        //var next = sortedDict[0];

        //�ش� ��ǥ�� Goal�̸� ����, �ƴϸ� ��� ����
        if (next.pos == goal)
        {
            print("Arrived");
            goalCell = next;

            return;
        }
        else
        {
            // �������� �������� Loop
            AStarTraverse(next);
        }






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