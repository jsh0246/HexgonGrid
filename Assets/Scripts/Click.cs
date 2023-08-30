using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera cam;
    public GameObject o;
    public Material M;
    public Grid grid;

    private void Start()
    {

    }

    private void Update()
    {
        MyClick();
    }

    private void MyClick()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
        //    {
        //        print(hit.collider.gameObject.transform.position);
        //        Material[] t = hit.collider.gameObject.GetComponent<MeshRenderer>().materials;

        //        hit.collider.gameObject.GetComponent<MeshRenderer>().material = M;
        //    }
        //}


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 worldPoint = ray.GetPoint(-ray.origin.y / ray.direction.y);
            //print(worldPoint);

            Vector3Int position = grid.WorldToCell(worldPoint);
            print(position);

            RaycastHit2D hit = Physics2D.Raycast(cam.transform.position, cam.transform.forward, Mathf.Infinity);
            print(hit);
            if (hit) {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
            }

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