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

            // ��ȯ ��ġ�� �ƴϰ� worldtocell ���ϰ� �׳� �����´�
            // �����°� �������� ������ ����� �͵��� ��ȭ���Ѿ� �ϴµ�?
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
