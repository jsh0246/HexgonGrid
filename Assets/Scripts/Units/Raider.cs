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

    // ������ ���� ��ǥ��°͵� �Լ� ó�� �ұ�?
    public void SkillQ(int q)
    {
        anim.SetTrigger(keyCodes[q].ToString());

        // ���ʹϾ� ������ �� ������µ� ���� �����ؾ���...
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

    // �̻��� �����Ǹ鼭 �ڱⰡ �ȸ°� �ϴ� ó����, �°Ե� �غ���
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

        // 5�ʵ�, Buff OFF
        StartCoroutine(BuffEnd(attackDamageBuff, e, 5f));
    }

    // ���� �������� ������� �ʴ� �ڷ�ƾ �Լ�
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

    // ���콺 Ŭ������ ���ѵȹ��� ������ Ŭ���Ͽ� Ÿ�� ����(�ٴڵ� ����)
    public void SkillR(int r)
    {
        anim.SetTrigger(keyCodes[0].ToString());

        GameObject meteor = Instantiate(skillEffects[r], transform.position + transform.forward * 4, Quaternion.identity);

        // ���׿� �������� ������ ����
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

            // �ڱ�� �ȸ°� (unit.gameObject != this.gameObject), ������ �����Ÿ��� �� ����� �������� ������
            //if (unit)
            if (unit && unit.gameObject != this.gameObject)
            {
                unit.GetDamage(skillDamage[r]);
            }
        }

        StartCoroutine(SkillEffectEnd(meteor, 5f));
    }
}


// bug1 : �̵������� ����� ���̶���Ʈ ���� �ʴ� ��찡 �ִ�. �ణ ����� �̻��ϰ� ���ڸ��̵����ָ� ��������
//          �Ƹ� (int) casting���� �ݿø��ø��������� ����ź���Ͻ����� �ݿø�? �Ǵ� ����ź���Ͻ� ����� �����ϴµ��� ������ �ִµ���