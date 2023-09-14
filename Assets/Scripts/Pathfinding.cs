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

    //private Player player;
    private Unit unit;

    private Vector2Int start, goal;
    private HashSet<Cell> openList;
    private HashSet<Cell> closedList;

    

    // �̷��� ���� pathfinding�� movecontroller�� ������ handshake�ϰ� �ִ°��� �ùٸ���?
    // �߰��� �����尰�� manager�� �־��ϳ�?
    MovingController mvctrl;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        //LeftClickGivesMousePosition();
        AStar();
    }

    private void InitVariables()
    {
        goalCell = null;

        //player = GetComponent<Player>();
        unit = GetComponent<Unit>();

        openList = new HashSet<Cell>(new EQ());
        closedList = new HashSet<Cell>(new EQ());

        mvctrl = GetComponent<MovingController>();
    }

    private void LeftClickGivesMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
            Vector2Int moousePoint = Calc.Vector3to2Int(GlobalGrid.Instance.Grid.WorldToCell(worldPoint));

            print("MOUSE POSITION : " + moousePoint);
        }
    }

    private void AStar()
    {
        if (unit.isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (SetAStarGoal()){

                    float g = 0;
                    float h = Calc.ManhattenDistance(start, goal);

                    Cell c = new Cell(start, g, h);
                    openList.Add(c);

                    //print("START POSITION : " + start);

                    AStarTraverse(c);

                    mvctrl.moveAllowed = true;
                    mvctrl.moveSetting = true;
                }
            }
        }
    }

    // ��Ŭ������ �� �ش� ��ǥ�� �����´�
    // �Լ� ���� �� ���� ���� ray.GetPoint? ray.origin.y?
    private bool SetAStarGoal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);

        start = unit.GetCurrentPosition();
        goal = Calc.Vector3to2Int(GlobalGrid.Instance.Grid.WorldToCell(worldPoint));

        //print("GOAL POSITION : " + goal);

        if (Calc.ManhattenDistance(start, goal) <= unit.moveRange)
        {
            return true;
        }
        else
            return false;


    }

    private void AStarTraverse(Cell c)
    {
        if(c.pos == goal)
        {
            print("gotcha");

            goalCell = c;

            openList.Clear();
            closedList.Clear();

            return;
        }

        openList.Remove(c);

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
                            //print("UPDATE");
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
     
        closedList.Add(c);

        openList = openList.OrderBy(x => x.f).ToHashSet();
        var next = openList.ElementAt(0);
        next.prevCell = c;

        Vector2Int dir = next.pos - c.pos;
        c.dir = new Vector3(dir.x, 0, dir.y);

        
        //print(next.PrintCell());

        //�ش� ��ǥ�� Goal�̸� ����, �ƴϸ� ��� ����
        if (next.pos == goal)
        {
            //print("Arrived");
            goalCell = next;

            openList.Clear();
            closedList.Clear();

            return;
        }
        else
        {
            // �������� �������� Loop
            AStarTraverse(next);
        }
    }
}