using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Calculation;

public class Player : Unit, ICharacter
{
    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    private void Update()
    {

    }

    private void InitVariables()
    {
        name = "Player 1";
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