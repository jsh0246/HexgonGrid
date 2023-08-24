using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Mesh mesh = new Mesh();

        //Vector3[] vertices = new Vector3[3];
        //Vector2[] uvs = new Vector2[3];
        //int[] triangles = new int[3];

        //vertices[0] = new Vector3(0, 0, 0);
        //vertices[1] = new Vector3(1, 0, 0);
        //vertices[2] = new Vector3(0, 0, 1);

        ////vertices[0] = new Vector3(0, 0, 0);
        ////vertices[1] = new Vector3(100, 0, 0);
        ////vertices[2] = new Vector3(0, 0, 100);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        //uvs[0] = new Vector2(0, 0);
        //uvs[1] = new Vector2(1, 0);
        //uvs[2] = new Vector2(0, 1);






        Vector3[] vertices = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(1, 0, 1);
        vertices[3] = new Vector3(0, 0, 1);

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
