using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Raider : Unit, ICharacter
{
    //[SerializeField] protected GameObject skillEffects
    //[SerializeField] private GameObject skillW;
    //[SerializeField] private GameObject skillE;
    //[SerializeField] private GameObject skillR;

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
        skillDamage = new int[4] { 10, 20, 10, 200 };
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

    // 데미지 범위 좌표잡는것도 함수 처리 할까?
    public void SkillQ(int q)
    {
        anim.SetTrigger(keyCodes[q].ToString());

        // 쿼터니언 개같이 찍어서 만들었는데 원리 이해해야함...
        GameObject stoneSlash = Instantiate(skillEffects[q], skillEffects[q].transform.position + transform.position + transform.forward * 2, Quaternion.Euler(transform.localEulerAngles - Vector3.up * 90));

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

        StartCoroutine(SkillEffectEnd(stoneSlash, 1f));
    }

    // 미사일 생성되면서 자기가 안맞게 하는 처리좀, 맞게도 해보기
    public void SkillW(int w)
    {
        anim.SetTrigger(keyCodes[0].ToString());

        //transform.rotation = Quaternion.LookRotation(Vector3.left);

        GameObject _missile = Instantiate(skillEffects[w], transform.position, transform.rotation);
        _missile.GetComponent<Bullet>().SetDamage(skillDamage[w]);
        _missile.GetComponent<Rigidbody>().AddForce(_missile.transform.forward * 10, ForceMode.Impulse);
    }

    public void SkillE(int e)
    {
        anim.SetTrigger(keyCodes[1].ToString());

        //GameObject _buff = Instantiate(buff, transform.position, transform.rotation);
        GameObject attackDamageBuff = Instantiate(skillEffects[e], transform);

        // Buff ON
        for(int i=0; i<4; i++)
        {
            if (i == e) continue;
            skillDamage[i] += skillDamage[e];
        }

        // 5초뒤, Buff OFF
        StartCoroutine(BuffEnd(attackDamageBuff, e, 5f));
    }

    // 아직 공용으로 사용하지 않는 코루틴 함수
    private IEnumerator BuffEnd(GameObject buff, int e, float duration)
    {
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < 4; i++)
        {
            if (i == e) continue;
            skillDamage[i] -= skillDamage[e];
        }

        Destroy(buff);
    }

    // 마우스 클릭으로 제한된범위 내에서 클릭하여 타겟 지정(바닥도 ㅇㅋ)
    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());

        GameObject meteor = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);

        // 메테오 범위내에 데미지 구현
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

        StartCoroutine(SkillEffectEnd(meteor, 5f));
    }
}


// bug1 : 이동범위가 제대로 하이라이트 되지 않는 경우가 있다. 약간 모양이 이상하게 제자리이동해주면 고쳐진다
//          아마 (int) casting에서 반올림올림문제인지 맨하탄디스턴스쪽의 반올림? 또는 맨하탄디스턴스 결과로 삭제하는데서 오류가 있는듯함