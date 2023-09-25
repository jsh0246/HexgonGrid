using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undead : Unit, ICharacter
{
    private GameObject undeadSkillE;
    private bool isTeleporting;
    private Vector3 teleportTarget;

    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    protected override void Update()
    {
        base.Update();

        if(isTeleporting )
        {
            Teleport();
        }
    }

    protected override void LateUpdate()
    {
        SkillCooldown();
    }

    private void InitVariables()
    {
        moveRange = 2;
        skills = new List<Skill> { SkillQ, SkillW, SkillE, SkillR };
        skillDamage = new int[4] { 10, 20, -1, 40 };
        isTeleporting = false;
    }

    protected override void SkillCooldown()
    {
        SkillShot();
        base.SkillCooldown();
    }

    private void SkillShot()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (isSelected && Input.GetKeyDown(keyCodes[i]) && !unitSkills.isCooldown[i])
            {
                skills[i](i);
                soundManager.UndeadSkillSound(i).Play();
            }
        }
    }

    public void SkillQ(int q)
    {
        anim.SetTrigger(keyCodes[q].ToString());
        GameObject undeadSkillQ = Instantiate(skillEffects[q], transform.position + transform.forward * 4, Quaternion.identity);

        Vector2Int gridPos = GetCurrentPosition();
        Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);
        Vector2Int skillRange = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z)) * 2;

        Unit unit = Squares.Instance.GetObject(squarePos + skillRange, Type.GetType("Unit")) as Unit;

        if (unit && unit.gameObject != this.gameObject)
        {
            unit.GetDamage(skillDamage[q]);
        }

        StartCoroutine(SkillEffectEnd(undeadSkillQ, 3f));
    }

    // 좌표잡는거 전부 개판, 그리드좌표랑 월드좌표랑 섞여서 더 개판
    public void SkillW(int w)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject[] undeadSkillW = new GameObject[3];

        Vector2Int gridPos = GetCurrentPosition();
        Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);
        Vector2Int[] skillRange = new Vector2Int[3];

        skillRange[0] = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z)) * 2;
        skillRange[1] = skillRange[0] + new Vector2Int(Mathf.RoundToInt(transform.right.x), Mathf.RoundToInt(transform.right.z));
        skillRange[2] = skillRange[0] + new Vector2Int(-Mathf.RoundToInt(transform.right.x), -Mathf.RoundToInt(transform.right.z));

        for (int i=0; i<undeadSkillW.Length; i++)
        {
            Vector3 v = transform.position + Calculation.Calc.Vector2to3Int(skillRange[i], 0) * 2;
            undeadSkillW[i] = Instantiate(skillEffects[w], v, Quaternion.identity);
        }

        foreach (var skill in skillRange)
        {
            Unit unit = Squares.Instance.GetObject(squarePos + skill, Type.GetType("Unit")) as Unit;

            if (unit)
            {
                unit.GetDamage(skillDamage[w]);
            }
        }

        for (int i=0;i<undeadSkillW.Length;i++)
        {
            StartCoroutine(SkillEffectEnd(undeadSkillW[i], 4f));
        }
    }

    public void SkillE(int e)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        undeadSkillE = Instantiate(skillEffects[e], skillEffects[e].transform.position + transform.position + transform.forward * 2, Quaternion.identity);

        StartCoroutine(SkillEffectEnd(undeadSkillE, 10f));
    }

    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject undeadSkillR = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);

        Vector2Int gridPos = GetCurrentPosition();
        Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);

        Vector2Int[,] skillRange = new Vector2Int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                skillRange[i, j] = new Vector2Int(i - 1, j - 1);
                skillRange[i, j] += new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z)) * 2;
            }
        }

        foreach (var skill in skillRange)
        {
            Unit unit = Squares.Instance.GetObject(squarePos + skill, Type.GetType("Unit")) as Unit;

            // 자기는 안맞게 (unit.gameObject != this.gameObject), 어차피 사정거리가 길어서 현재는 맞을일은 없지만
            //if (unit)
            if (unit && unit.gameObject != this.gameObject)
            {
                unit.GetDamage(skillDamage[r]);
            }
        }

        StartCoroutine(SkillEffectEnd(undeadSkillR, 5f));
    }

    // 수정 필요
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == undeadSkillE)
        {
            if (GetComponent<MovingController>().moveAllowed == false)
            {
                isTeleporting = true;

                // 최대거리가 3(6/2)로 하기, 그리드 밖으로 벗어나지 않기(Mathf.Clamp)
                teleportTarget = transform.position + transform.forward * 6;
                teleportTarget = new Vector3(Mathf.Clamp(teleportTarget.x, -13, 17), teleportTarget.y, Mathf.Clamp(teleportTarget.z, -13, 17));
            }
        }
    }

    private void Teleport()
    {
        transform.position = Vector3.MoveTowards(transform.position, teleportTarget, 1f);
        if(transform.position == teleportTarget)
        {
            isTeleporting = false;
        }
    }
}
