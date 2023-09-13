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
        HashSet<string> list = new HashSet<string>();

        list.Add("ABC");
        list.Add("ABC");

        list.Add("ABC");
        list.Add("ABC");

        list.Add("ABCCC");

        print(list.Count);

    }
}