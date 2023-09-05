using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Cell
{
    public Vector2Int pos;
    public float f, g, h;

    public Cell(Vector2Int pos, float g, float h)
    {
        this.pos = pos;
        this.g = g;
        this.h = h;

        this.f = g + h;
    }
}

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    private Player player;
    private Vector2Int goal;

    private float f, g, n;
    private List<Vector2Int> openList, closedList;

    private void Start()
    {
        player = GetComponent<Player>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();

        openList = new List<Vector2Int>();
        closedList = new List<Vector2Int>();
    }

    private void Update()
    {
        SetGoal();

        AStar();
    }

    private void SetGoal()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
            Vector2Int goal = Vector3to2Int(grid.WorldToCell(worldPoint));
            

            print(goal);

            //tilemap.SetTile(position, tilebase);
        }
    }

    private void AStar()
    {
        Vector2Int pos = new Vector2Int(player.currentPos.x, player.currentPos.z);
        float g = 0;
        float h = ManhattenDistance(pos, goal);

        Cell start = new Cell(pos, g, h);

        while (true)
        {
            

     
        }
    }

    private Vector2Int Vector3to2Int(Vector3Int v)
    {
        return new Vector2Int(v.x, v.z);
    }

    private int ManhattenDistance(Vector2Int s, Vector2Int t)
    {
        return Mathf.Abs(s.x - t.x) + Mathf.Abs(s.y - t.y);
    }
}
