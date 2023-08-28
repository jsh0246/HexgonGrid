using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    private Camera cam;
    public GameObject o;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MyClick();
    }

    private void MyClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z);
            Vector3 mP = Input.mousePosition;

            //print("MousePosition " + mPos);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mPos);
            //Vector3Int pos = grid.WorldToCell(mouseWorldPos);

            print("MouseWorldPosition : " + mouseWorldPos);

            Instantiate(o, mouseWorldPos, Quaternion.identity);
            //print(pos);
        }
    }

    //private void OnGUI()
    //{
    //    Vector3 point = new Vector3();
    //    Event currentEvent = Event.current;
    //    Vector2 mousePos = new Vector2();

    //    // Get the mouse position from Event.
    //    // Note that the y position from Event is inverted.
    //    mousePos.x = currentEvent.mousePosition.x;
    //    mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

    //    point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

    //    GUILayout.BeginArea(new Rect(20, 20, 250, 120));
    //    GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
    //    GUILayout.Label("Mouse position: " + mousePos);
    //    GUILayout.Label("World position: " + point.ToString("F3"));
    //    GUILayout.EndArea();
    //}
}