using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Test1();
    }

    private void Update()
    {

    }

    private void Test1()
    {
        //Squares.Instance.GetSquare(3, 3).floor.GetComponent<MeshRenderer>().material.color = Color.magenta;
        //print(Squares.Instance.GetUnit(4, 3).name);
        print(Squares.Instance.GetUnit(3, 8));
        print(Squares.Instance.GetUnit(5, 8));

    }
}