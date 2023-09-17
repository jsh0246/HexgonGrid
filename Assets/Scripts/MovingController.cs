using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingController : MonoBehaviour
{
    [HideInInspector]
    public bool moveSetting;
    [HideInInspector]
    public bool moveAllowed;

    private Unit unit;

    private Pathfinding pf;
    private List<Cell> path;

    private int cnt;

    private Vector3Int goal;
    private Vector3 vGoal;

    private void Start()
    {
        InitVariables();
    }

    private void FixedUpdate()
    {
        MakePathAndMove();
    }

    private void InitVariables()
    {
        unit = GetComponent<Unit>();

        pf = GetComponent<Pathfinding>();
        path = new List<Cell>();

        moveAllowed = false;
        moveSetting = false;
    }

    private void MakePathAndMove()
    {
        if (unit.isSelected)
        {
            if (moveAllowed)
            {
                if (moveSetting)
                {
                    MakePath();
                    MoveSettings();
                    moveSetting = false;
                }

                Move();
            }
        }
    }

    private void MakePath()
    {
        Cell cell = pf.goalCell;

        while (cell != null)
        {
            path.Add(cell);
            cell = cell.prevCell;
        }

        path.Reverse();

        foreach (Cell c in path)
            print(c.PrintCell());
    }

    private void MoveSettings()
    {
        cnt = 0;

        goal = new Vector3Int(path[cnt].pos.x, path[cnt].pos.y, 0);

        vGoal = GlobalGrid.Instance.Grid.CellToWorld(goal);
        vGoal += (Vector3.right + Vector3.up * unit.transform.position.y + Vector3.forward);
        cnt++;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, vGoal, Time.deltaTime * 5f);
        // ÇÑÄ­ÇÑÄ­ÀÌµ¿ÇÒ¶§¸¶´Ù LookRotationÀ» Àâ¾ÆÁà¾ßÇÏ´Âµ¥?


        if (transform.position == vGoal)
        {
            //transform.rotation = Quaternion.LookRotation(path[cnt].dir);

            if (cnt == path.Count)
            {
                //print("µµÂø");
                moveAllowed = false;

                path.Clear();
                return;
            }
            
            goal = new Vector3Int(path[cnt].pos.x, path[cnt].pos.y, 0);

            vGoal = GlobalGrid.Instance.Grid.CellToWorld(goal);
            vGoal += (Vector3.right + Vector3.up * unit.transform.position.y + Vector3.forward);

            if (path[cnt].dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(path[cnt].dir);

            cnt++;
            print(cnt);
        }
    }


}