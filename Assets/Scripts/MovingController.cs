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

    [SerializeField]
    private Grid grid;

    private Pathfinding pf;
    private List<Cell> path;

    private int cnt;

    private Vector3Int goal;
    private Vector3 vGoal;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        if (moveAllowed)
        {
            if(moveSetting)
            {
                MakePath();
                MoveSettings();
                moveSetting = false;
            }

            Move();
        }
    }

    private void InitVariables()
    {
        pf = GetComponent<Pathfinding>();
        path = new List<Cell>();

        moveAllowed = false;
        moveSetting = false;
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

        //foreach (Cell c in path)
        //    print(c.PrintCell());
    }

    private void MoveSettings()
    {
        cnt = 0;

        goal = new Vector3Int(path[cnt].pos.x, path[cnt].pos.y, 0);

        vGoal = grid.CellToWorld(goal);
        vGoal += (Vector3.right + Vector3.up * 2 + Vector3.forward);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, vGoal, Time.deltaTime * 5f);
        if (transform.position == vGoal)
        {
            if (cnt == path.Count)
            {
                print("µµÂø");
                moveAllowed = false;

                path.Clear();
                return;
            }
            
            goal = new Vector3Int(path[cnt].pos.x, path[cnt].pos.y, 0);

            vGoal = grid.CellToWorld(goal);
            vGoal += (Vector3.right + Vector3.up * 2 + Vector3.forward);

            cnt++;
        }
    }


}