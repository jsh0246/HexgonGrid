using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Raider : Unit, ICharacter
{
    [SerializeField] private GameObject skillW;
    [SerializeField] private GameObject skillE;
    [SerializeField] private GameObject skillR;

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
        moveRange = 3;
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
        for(int i=0; i<keyCodes.Length; i++)
        {
            if (isSelected && Input.GetKeyDown(keyCodes[i]) && !unitSkills.isCooldown[i])
            {
                skills[i](i);
                soundManager.RaiderSkillSound(i).Play();
            }
        }
    }

    public void SkillQ(int q)
    {
        anim.SetTrigger("Q");

        Vector2Int gridPos = GetCurrentPosition();
        Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);

        Vector2Int[] skillRange = new Vector2Int[4];
        Vector2Int v1 = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z));
        Vector2Int v2 = new Vector2Int(Mathf.RoundToInt(transform.right.x), Mathf.RoundToInt(transform.right.z));
        Vector2Int v3 = new Vector2Int(-Mathf.RoundToInt(transform.right.x), -Mathf.RoundToInt(transform.right.z));

        skillRange[0] = v1;
        skillRange[1] = v1 + v1;
        skillRange[2] = skillRange[1] + v2;
        skillRange[3] = skillRange[1] + v3;

        //foreach (var skill in skillRange)
        //    print(skill);

        foreach (var skill in skillRange) {
            Unit unit = Squares.Instance.GetObject(squarePos + skill, Type.GetType("Unit")) as Unit;
            //print(unit);
            //print(squarePos + skill);

            if (unit)
            {
                unit.GetDamage(skillDamage[q]);
            }
        }
    }

    public void SkillW(int w)
    {
        anim.SetTrigger("W");

        //transform.rotation = Quaternion.LookRotation(Vector3.left);

        GameObject _missile = Instantiate(skillW, transform.position, transform.rotation);
        _missile.GetComponent<Bullet>().SetDamage(skillDamage[w]);
        _missile.GetComponent<Rigidbody>().AddForce(_missile.transform.forward * 10, ForceMode.Impulse);
    }

    public void SkillE(int e)
    {
        anim.SetTrigger("Q");

        //GameObject _buff = Instantiate(buff, transform.position, transform.rotation);
        GameObject _buff = Instantiate(skillE, transform);

        // Buff ON
        for(int i=0; i<4; i++)
        {
            if (i == e) continue;
            skillDamage[i] += skillDamage[e];
        }

        // 5초뒤, Buff OFF
        StartCoroutine(BuffOff(e, _buff));
    }

    private IEnumerator BuffOff(int e, GameObject _buff)
    {
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 4; i++)
        {
            if (i == e) continue;
            skillDamage[i] -= skillDamage[e];
        }

        Destroy(_buff);
    }

    // 마우스 클릭으로 제한된범위 내에서 클릭하여 타겟 지정(바닥도 ㅇㅋ)
    public void SkillR(int r)
    {
        anim.SetTrigger("Q");

        GameObject _meteor = Instantiate(skillR, transform);

        // 메테오 범위내에 데미지 구현

        StartCoroutine(MeteorEnd(_meteor));
    }

    private IEnumerator MeteorEnd(GameObject _buff)
    {
        yield return new WaitForSeconds(5f);

        Destroy(_buff);
    }
}


// bug1 : 이동범위가 제대로 하이라이트 되지 않는 경우가 있다. 약간 모양이 이상하게 제자리이동해주면 고쳐진다
//          아마 (int) casting에서 반올림올림문제인지 맨하탄디스턴스쪽의 반올림? 또는 맨하탄디스턴스 결과로 삭제하는데서 오류가 있는듯함