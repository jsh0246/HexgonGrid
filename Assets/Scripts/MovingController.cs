using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingController : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private Pathfinding pf;
    private List<Cell> path;

    private Vector2Int startPos, goalPos;
    private float timeToMove = 1f;
    private bool moveAllowed;

    private void Start()
    {
        InitVariables();
        MakePath();
    }

    private void Update()
    {
        if (path.Count > 1 && !moveAllowed)
        {
            StartCoroutine(MoveOneCellCor());
        }
    }

    private void InitVariables()
    {
        pf = GetComponent<Pathfinding>();
        path = new List<Cell>();

        moveAllowed = false;
    }

    private void MakePath()
    {
        Cell cell = pf.goalCell;

        while(cell != null)
        {
            path.Add(cell);
            cell = cell.prevCell;
        }

        path.Reverse();
    }

    private IEnumerator MoveOneCellCor()
    {
        moveAllowed = true;

        float elaspedTime = 0f;

        startPos = path[0].pos;
        goalPos = path[1].pos;
        while (elaspedTime < timeToMove) {
            Vector2 v = Vector2.Lerp(startPos, goalPos, (elaspedTime / timeToMove));
            v *= grid.cellSize;

            transform.position = new Vector3(v.x, transform.position.y, v.y);

            elaspedTime += Time.deltaTime;

            yield return null;
        }

        path.RemoveAt(0);
        moveAllowed = false;
    }
}