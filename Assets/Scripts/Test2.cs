using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : Test
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        lollipop = -100;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();

        //POPPOP();
    }

    private void POPPOP()
    {
        print(lollipop);
    }
}
