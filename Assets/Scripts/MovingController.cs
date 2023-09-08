using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingController : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private Pathfinding pf;
    private List<Cell> path;

    private Vector2Int startPos, goalPos;
    private float timeToMove;
    private bool moveAllowed;

    private int cnt;

    private Vector3 start, goal;

    private void Start()
    {
        InitVariables();
        MakePath();

        goal = Calculation.Calc.Vector2to3Int(path[cnt].pos, transform.position.y);
        goal.x *= grid.cellSize.x;
        goal.z *= grid.cellSize.y;

        print(cnt + " : " + goal);
    }

    private void Update()
    {
        //if (path.Count > 2 && !moveAllowed)
        //{
        //    StartCoroutine(MoveOneCellCor());
        //}
        if (moveAllowed)
        {
            MV();
        }
    }

    private void InitVariables()
    {
        pf = GetComponent<Pathfinding>();
        path = new List<Cell>();

        timeToMove = 1f;
        moveAllowed = true;

        cnt = 0;
    }

    private void MakePath()
    {
        Cell cell = pf.goalCell;

        while(cell != null)
        {
            path.Add(cell);
            //print("MAKING PATH : " + cell.PrintCell());
            cell = cell.prevCell;
        }

        path.Reverse();

        foreach (Cell c in path)
            print(c.PrintCell());
    }

    private IEnumerator MoveOneCellCor()
    {
        moveAllowed = true;

        float elaspedTime = 0f;

        startPos = path[0].pos;
        goalPos = path[1].pos;
        while (elaspedTime < timeToMove) {
            Vector2 v = Vector2.Lerp(startPos, goalPos, (elaspedTime / timeToMove));
            //v *= grid.cellSize;
            v.x *= grid.cellSize.x;
            v.y *= grid.cellSize.y;

            transform.position = new Vector3(v.x, transform.position.y, v.y);

            elaspedTime += Time.deltaTime;

            yield return null;
        }

        //cnt++;
        //transform.position = Calculation.Calc.Vector2to3Int(goalPos, transform.position.y) * new Vector3Int((int)grid.cellSize.x, 1, (int)grid.cellSize.y);

        //print("GIGI" + cnt);
        path.RemoveAt(0);
        moveAllowed = false;
    }

    private void MV()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, Time.deltaTime * 5f);
        if (transform.position == goal)
        {
            if (cnt == path.Count)
            {
                print("µµÂø");
                moveAllowed = false;
                return;
            }

            cnt++;
            goal = Calculation.Calc.Vector2to3Int(path[cnt].pos, transform.position.y);
            goal.x *= grid.cellSize.x;
            goal.z *= grid.cellSize.y;

            print(cnt + " : " + goal);
        }
    }
}