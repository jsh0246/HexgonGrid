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

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SkillQ()
    {
        throw new System.NotImplementedException();
    }

    public void SkillW()
    {
        throw new System.NotImplementedException();
    }

    public void SkillE()
    {
        throw new System.NotImplementedException();
    }

    public void SkillUltR()
    {
        throw new System.NotImplementedException();
    }
}