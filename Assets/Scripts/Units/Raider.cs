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

        // 5�ʵ�, Buff OFF
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

    // ���콺 Ŭ������ ���ѵȹ��� ������ Ŭ���Ͽ� Ÿ�� ����(�ٴڵ� ����)
    public void SkillR(int r)
    {
        anim.SetTrigger("Q");

        GameObject _meteor = Instantiate(skillR, transform);

        // ���׿� �������� ������ ����

        StartCoroutine(MeteorEnd(_meteor));
    }

    private IEnumerator MeteorEnd(GameObject _buff)
    {
        yield return new WaitForSeconds(5f);

        Destroy(_buff);
    }
}


// bug1 : �̵������� ����� ���̶���Ʈ ���� �ʴ� ��찡 �ִ�. �ణ ����� �̻��ϰ� ���ڸ��̵����ָ� ��������
//          �Ƹ� (int) casting���� �ݿø��ø��������� ����ź���Ͻ����� �ݿø�? �Ǵ� ����ź���Ͻ� ����� �����ϴµ��� ������ �ִµ���