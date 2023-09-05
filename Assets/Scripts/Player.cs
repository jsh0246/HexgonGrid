using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Grid grid;
    public Vector3Int currentPos { get; private set; }

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        currentPos = grid.WorldToCell(transform.position);
    }

    private void Update()
    {

    }
}
