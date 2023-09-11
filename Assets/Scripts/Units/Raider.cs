using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raider : Unit, ICharacter
{
    private MovingController mv;
    private Animator anim;

    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    private void Update()
    {
        Walk();
        DrawMoveRange();
    }

    private void InitVariables()
    {
        name = "Raider";
        maxHp = currentHp = 100f;
        moveRange = 3;

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();
    }

    public override void DrawMoveRange()
    {
        if (isSelected)
        {
            base.DrawMoveRange();

            Vector3Int curPos = GetCurrentPositionVector3Int();

            // 변환 위치가 아니고 worldtocell 안하고 그냥 가져온다
            // 가져온걸 기준으로 마름모 모양의 것들을 변화시켜야 하는데?
        }
    }

    private void Walk()
    {
        if(mv.moveAllowed)
        {
            anim.SetBool("isWalk", true);
        }
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
