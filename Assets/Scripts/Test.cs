using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private void Start()
    {
        //Test1();
        //Test2();
        Test3();
    }

    private void Test1()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uvs = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 0, 1);

        //vertices[0] = new Vector3(0, 0, 0);
        //vertices[1] = new Vector3(100, 0, 0);
        //vertices[2] = new Vector3(0, 0, 100);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);






        //Vector3[] vertices = new Vector3[4];
        //Vector2[] uvs = new Vector2[4];
        //int[] triangles = new int[6];

        //vertices[0] = new Vector3(0, 0, 0);
        //vertices[1] = new Vector3(1, 0, 0);
        //vertices[2] = new Vector3(1, 0, 1);
        //vertices[3] = new Vector3(0, 0, 1);

        //triangles[0] = 0;
        //triangles[1] = 2;
        //triangles[2] = 1;
        //triangles[3] = 0;
        //triangles[4] = 3;
        //triangles[5] = 2;

        //uvs[0] = new Vector2(0, 0);
        //uvs[1] = new Vector2(1, 0);
        //uvs[2] = new Vector2(1, 1);
        //uvs[3] = new Vector2(0, 1);

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
    private void Test2()
    {
        //Dictionary<Vector2Int, Cell> aStarGrid = new Dictionary<Vector2Int, Cell>();

        //Cell c1 = new Cell(new Vector2Int(0, 0), 0, 5);
        //Cell c2 = new Cell(new Vector2Int(0, 0), 1, 6);
        //Cell c3 = new Cell(new Vector2Int(0, 0), 2, 7);
        //Cell c4 = new Cell(new Vector2Int(0, 0), 3, 8);
        //Cell c5 = new Cell(new Vector2Int(0, 0), 4, 9);



        //aStarGrid.Add(c1);

        //foreach (KeyValuePair<Vector2Int, Cell> pair in aStarGrid)
        //{
        //    print(pair.Key);
        //    pair.Value.PrintCell();
        //}
    }

    private void Test3()
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();

        dict.Add(0, "조상치");
        dict.Add(1, "김상치");
        dict.Add(2, "김상치");
        dict.Add(3, "이상치");
        dict.Add(4, "박상치");

        try
        {
            dict.Add(0, "정상치");
        } catch(ArgumentException e)
        {
            print("do sth");
            print(e);
        }
    }
}
