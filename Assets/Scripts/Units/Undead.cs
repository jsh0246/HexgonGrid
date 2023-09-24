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

    protected override void LateUpdate()
    {
        SkillCooldown();
    }

    private void InitVariables()
    {
        moveRange = 1;
        skills = new List<Skill> { SkillQ, SkillW, SkillE, SkillR };
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
            }
        }
    }

    public void SkillQ(int q)
    {
        anim.SetTrigger("Q");

        print(name + "Q Skill");
    }

    public void SkillW(int w)
    {
        print(name + "W Skill");
    }

    public void SkillE(int e)
    {
        print(name + "E Skill");
    }

    public void SkillR(int r)
    {
        print(name + "R Skill");
    }
}
