using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit, ICharacter
{
    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    protected override void Update()
    {

    }

    private void InitVariables()
    {

    }

    /* SKills */

    private void SummonCommonoMinion1()
    {

    }

    private void SummonCommonoMinion2()
    {

    }

    private void SummonUncommonMinion1()
    {

    }

    public void SkillQ(int q)
    {
        throw new System.NotImplementedException();
    }

    public void SkillW(int w)
    {
        throw new System.NotImplementedException();
    }

    public void SkillE(int e)
    {
        throw new System.NotImplementedException();
    }

    public void SkillR(int r)
    {
        throw new System.NotImplementedException();
    }
}