using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    protected int lollipop;

    protected virtual void Start()
    {
        //lollipop = 100;

        //POP();
    }

    protected virtual void Update()
    {
        POP();
    }

    protected void POP()
    {
        print(lollipop);
    }
}