using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Footman : Unit, ICharacter
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
        skillDamage = new int[4] { 10, 20, 10, 40 };
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
                soundManager.FootmanSkillSound(i).Play();
            }
        }
    }

    public void SkillQ(int q)
    {
        anim.SetTrigger(keyCodes[q].ToString());
        GameObject _stoneSlash = Instantiate(skillEffects[q]);
    }

    public void SkillW(int w)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject _stoneSlash = Instantiate(skillEffects[w]);
    }

    public void SkillE(int e)
    {
        anim.SetTrigger(keyCodes[1].ToString());
        GameObject _stoneSlash = Instantiate(skillEffects[e]);
    }

    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject _stoneSlash = Instantiate(skillEffects[r]);
    }
}
