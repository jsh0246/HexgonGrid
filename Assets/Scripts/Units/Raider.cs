using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raider : Object, ICharacter
{

    private Grid grid;
    private MovingController mv;
    private Animator anim;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        Walk();
    }

    private void InitVariables()
    {
        name = "Raider";

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Walk()
    {
        if(mv.moveAllowed)
        {
            anim.SetBool("isWalk", true);
        }
    }

    public Vector2Int GetCurrentPosition()
    {
        currentPos = grid.WorldToCell(transform.position);

        //print(Calc.Vector3to2Int(currentPos));

        return Calc.Vector3to2Int(currentPos);
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
