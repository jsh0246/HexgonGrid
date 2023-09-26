using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player : Unit, ICharacter
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
        moveRange = 5;
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
                soundManager.PlayerSkillSound(i).Play();
            }
        }
    }

    public void SkillQ(int q)
    {
        anim.SetTrigger(keyCodes[q].ToString());
        //anim.SetBool("iSsummoning", true);
        StartCoroutine(Summon(q));
    }

    public void SkillW(int w)
    {
        anim.SetTrigger(keyCodes[0].ToString());
        StartCoroutine(Summon(w));
    }

    public void SkillE(int e)
    {
        anim.SetTrigger(keyCodes[1].ToString());
        StartCoroutine(Summon(e));
    }

    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[r].ToString());
        //GameObject footmanSkillR = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);

        //StartCoroutine(SkillEffectEnd(footmanSkillR, 8f));
    }

    private IEnumerator Summon(int n)
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Area")))
                {
                    Vector3 pos = hit.transform.position;
                    pos.y = 1f;
                    GameObject footmanSkillW = Instantiate(skillEffects[n], pos, Quaternion.identity);
                    //anim.SetBool("iSsummoning", false);

                    yield break;
                }
            }
            yield return null;
        }
    }
}