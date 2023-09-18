using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : Unit, ICharacter
{

    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void InitVariables()
    {
        moveRange = 2;
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SkillE()
    {
        throw new System.NotImplementedException();
    }

    public void SkillQ()
    {
        throw new System.NotImplementedException();
    }

    public void SkillUltR()
    {
        throw new System.NotImplementedException();
    }

    public void SkillW()
    {
        throw new System.NotImplementedException();
    }
}
