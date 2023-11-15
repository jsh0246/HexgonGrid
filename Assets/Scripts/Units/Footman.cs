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
        GameObject footmanSkillQ = Instantiate(skillEffects[q], transform.position + transform.forward * 2, Quaternion.identity);

        StartCoroutine(SkillEffectEnd(footmanSkillQ, 1f));
    }

    public void SkillW(int w)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject footmanSkillW = Instantiate(skillEffects[w], transform.position + transform.forward * 4, Quaternion.identity);

        StartCoroutine(SkillEffectEnd(footmanSkillW, 1f));
    }

    public void SkillE(int e)
    {
        anim.SetTrigger(keyCodes[1].ToString());
        GameObject footmanSkillE = Instantiate(skillEffects[e], transform.position + transform.forward * 4, Quaternion.identity);

        StartCoroutine(SkillEffectEnd(footmanSkillE, 5f));
    }

    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        GameObject footmanSkillR = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);

        StartCoroutine(SkillEffectEnd(footmanSkillR, 8f));
    }
}