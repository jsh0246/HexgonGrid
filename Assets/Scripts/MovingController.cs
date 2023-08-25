using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingController : MonoBehaviour
{
    public Tilemap map;


    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * 0.5f;
        }
    }

    private void MoveMove()
    {
        transform.position += new Vector3(0.5f, 0, 0);

    }
}
