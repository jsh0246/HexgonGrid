using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    protected override void LateUpdate()
    {
        SkillCooldown();
    }

    private void InitVariables()
    {
        moveRange = 2;
        skills = new List<Skill> { SkillQ, SkillW, SkillE, SkillR };
        skillDamage = new int[4] { 10, 20, -1, 40 };
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
        GameObject _stoneSlash = Instantiate(skillEffects[e], transform.position + transform.forward * 4, Quaternion.identity);
    }

    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject _stoneSlash = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);
    }
}
